using EternalFortress.Entities.DTOs;

namespace EternalFortress.Business.Files
{
    public interface IFileFacade
    {
        IEnumerable<FileInfoDTO> GetUserFiles(int userId, int folderId);
    }
}