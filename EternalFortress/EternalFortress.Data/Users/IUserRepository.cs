using EternalFortress.Entities.DTOs;

namespace EternalFortress.Data.Users
{
    public interface IUserRepository
    {
        UserDTO GetUserById(int id);
    }
}