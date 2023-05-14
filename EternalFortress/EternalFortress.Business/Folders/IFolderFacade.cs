using EternalFortress.Entities.DTOs;

namespace EternalFortress.Business.Folders
{
    public interface IFolderFacade
    {
        Task CreateFolder(int userId, FolderDTO folder);
        IEnumerable<FolderDTO> GetUserFolders(int userId);
    }
}