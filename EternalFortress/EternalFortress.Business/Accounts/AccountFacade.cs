using EternalFortress.Business.Services;
using EternalFortress.Data.Users;
using EternalFortress.Entities.DTOs;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace EternalFortress.Business.Accounts
{
    public class AccountFacade : IAccountFacade
    {
        private readonly IJwtService _jwtService;
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;

        public AccountFacade(IJwtService jwtService, IConfiguration configuration, IUserRepository userRepository)
        {
            _jwtService = jwtService;
            _configuration = configuration;
            _userRepository = userRepository;
        }

        public void Register(UserDTO user)
        {
            var encryptionKey = _configuration["EncryptionKey"];
            var userExists = _userRepository.GetUserByEmail(user.Email) != null;

            if (userExists)
                throw new ArgumentException("A user with this email address already exists");

            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(encryptionKey)))
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(user.Password);
                byte[] hashBytes = hmac.ComputeHash(inputBytes);
                user.Password = BitConverter.ToString(hashBytes).Replace("-", "");

                _userRepository.SaveUser(user);
            }
        }

        public bool Login(string email, string password)
        {
            var encryptionKey = _configuration["EncryptionKey"];

            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(encryptionKey)))
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = hmac.ComputeHash(inputBytes);
                string hashPassword = BitConverter.ToString(hashBytes).Replace("-", "");

                var userPassword = _userRepository.GetPasswordHashByEmail(email);
                return userPassword == hashPassword;
            }
        }

        public string GetToken(string email)
        {
            var user = _userRepository.GetUserByEmail(email);

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                };

            var token = _jwtService.GenerateToken(claims);
            return token;
        }
    }
}
