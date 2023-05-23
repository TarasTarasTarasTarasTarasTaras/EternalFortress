using EternalFortress.Entities.DTOs;

namespace EternalFortress.Business.Files
{
    public interface IFileFacade
    {
        IEnumerable<FileInfoDTO> GetUserFiles(int userId, int folderId);
        int SaveFileInfo(FileInfoDTO fileInfo);
        Task UploadChunkToS3(int userId, ChunkDTO dto);
        Task<string> DownloadChunkFromS3(int userId, int fileId, int index);
    }
}