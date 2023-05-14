using Amazon.S3;
using AutoMapper;
using EternalFortress.API.Extensions;
using EternalFortress.API.Models;
using EternalFortress.Business.Folders;
using EternalFortress.Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace EternalFortress.API.Controllers
{
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IFolderFacade _folderFacade;

        public DashboardController(IMapper mapper, IFolderFacade folderFacade)
        {
            _mapper = mapper;
            _folderFacade = folderFacade;
        }

        [HttpGet("get-user-folders")]
        public IActionResult GetAllFolders()
        {
            var folders = _folderFacade.GetUserFolders(User.GetId());
            return Ok(folders);
        }

        [HttpPost("create-folder")]
        public async Task<IActionResult> CreateFolder([FromBody] FolderModel model)
        {
            model.UserId = User.GetId();
            var folder = _mapper.Map<FolderDTO>(model);

            await _folderFacade.CreateFolder(User.GetId(), folder);
            return Ok();
        }
    }
}
