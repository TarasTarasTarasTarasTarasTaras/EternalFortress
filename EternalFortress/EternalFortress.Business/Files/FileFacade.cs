using Amazon.S3;
using EternalFortress.Data.Files;
using EternalFortress.Entities.DTOs;
using Microsoft.Extensions.Configuration;

namespace EternalFortress.Business.Files
{
    public class FileFacade : IFileFacade
    {
        private readonly IAmazonS3 _s3Client;
        private readonly IConfiguration _configuration;
        private readonly IFileRepository _fileRepository;

        public FileFacade(IAmazonS3 s3Client, IConfiguration configuration, IFileRepository fileRepository)
        {
            _s3Client = s3Client;
            _configuration = configuration;
            _fileRepository = fileRepository;
        }

        public IEnumerable<FileInfoDTO> GetUserFiles(int userId, int folderId)
        {
            var files = _fileRepository.GetUserFiles(userId, folderId);
            return files;
        }
    }
}
