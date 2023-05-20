using AutoMapper;
using EternalFortress.API.Extensions;
using EternalFortress.API.Models;
using EternalFortress.Business.Files;
using EternalFortress.Business.Folders;
using EternalFortress.Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace EternalFortress.API.Controllers
{
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IFileFacade _fileFacade;
        private readonly IFolderFacade _folderFacade;

        public DashboardController(IMapper mapper, IFileFacade fileFacade, IFolderFacade folderFacade)
        {
            _mapper = mapper;
            _fileFacade = fileFacade;
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

        [HttpPost("save-file-info")]
        public IActionResult SaveFileInfo([FromBody] FileInfoModel model)
        {
            model.UserId = User.GetId();
            model.UploadDate = DateTime.Now;

            var fileId = _fileFacade.SaveFileInfo(_mapper.Map<FileInfoDTO>(model));
            return Ok(fileId);
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadChunk([FromForm] ChunkModel model)
        {
            var dto = _mapper.Map<ChunkDTO>(model);
            await _fileFacade.UploadChunkToS3(User.GetId(), dto);
            
            return Ok();
        }
    }
}
