using AutoMapper;
using EternalFortress.Data.EF.Context;
using EternalFortress.Data.EF.Entities;
using EternalFortress.Entities.DTOs;

namespace EternalFortress.Data.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly IMapper _mapper;
        private readonly EternalFortressContext Context;

        public UserRepository(EternalFortressContext context, IMapper mapper)
        {
            _mapper = mapper;
            Context = context;
        }

        public UserDTO GetUserById(int id)
        {
            var user = Context.Users.FirstOrDefault(u => u.Id == id);

            return _mapper.Map<UserDTO>(user);
        }

        public UserDTO GetUserByEmail(string email)
        {
            var user = Context.Users.FirstOrDefault(u => u.Email == email);

            if (user == null) return null;

            return _mapper.Map<UserDTO>(user);
        }

        public void SaveUser(UserDTO user)
        {
            var entity = _mapper.Map<User>(user);

            Context.Users.Add(entity);
            Context.SaveChanges();
        }

        public string GetPasswordHashByEmail(string email)
        {
            var user = Context.Users.FirstOrDefault(u => u.Email == email);
            return user.Password;
        }
    }
}
