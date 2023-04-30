using AutoMapper;
using EternalFortress.Data.EF.Context;
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
    }
}
