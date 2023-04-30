using EternalFortress.Entities.DTOs;

namespace EternalFortress.Data.Users
{
    public interface IUserRepository
    {
        UserDTO GetUserById(int id);

        UserDTO GetUserByEmail(string email);

        void SaveUser(UserDTO user);

        string GetPasswordHashByEmail(string email);
    }
}