using EternalFortress.Entities.DTOs;

namespace EternalFortress.Data.Files
{
    public interface IFileRepository
    {
        IEnumerable<FileInfoDTO> GetUserFiles(int userId, int folderId);
        int SaveFileInfo(FileInfoDTO file);
    }
}