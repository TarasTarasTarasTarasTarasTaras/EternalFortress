using AutoMapper;
using EternalFortress.API.Models;
using EternalFortress.Business.Accounts;
using EternalFortress.Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace EternalFortress.API.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAccountFacade _accountFacade;

        public AccountController(IAccountFacade accountFacade, IMapper mapper)
        {
            _mapper = mapper;
            _accountFacade = accountFacade;
        }

        [HttpPost("register")]
        public ActionResult Register([FromBody] RegisterModel model)
        {
            var user = _mapper.Map<UserDTO>(model);
            _accountFacade.Register(user);

            return Ok();
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginModel model)
        {
            var logined = _accountFacade.Login(model.Email, model.Password);

            if (!logined)
                return Unauthorized("Incorrect email or password");

            var token = _accountFacade.GetToken(model.Email);
            return Ok(new { token = token });
        }

        [HttpGet("email-exists")]
        public ActionResult GetEmailExists([FromQuery] string email)
        {
            var exists = _accountFacade.UserAlreadyExists(email);
            return Ok(exists);
        }

        [HttpGet("countries")]
        public ActionResult GetCountries()
        {
            var countries = _accountFacade.GetCountries();
            return Ok(countries);
        }
    }
}
