using EternalFortress.Entities.DTOs;

namespace EternalFortress.Business.Accounts
{
    public interface IAccountFacade
    {
        string GetToken(string email);

        bool Login(string email, string password);

        void Register(UserDTO user);

        bool UserAlreadyExists(string email);

        IEnumerable<CountryDTO> GetCountries();
    }
}