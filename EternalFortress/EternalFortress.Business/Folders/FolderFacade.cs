using Amazon.S3;
using Amazon.S3.Model;
using EternalFortress.Data.Folders;
using EternalFortress.Entities.DTOs;
using Microsoft.Extensions.Configuration;

namespace EternalFortress.Business.Folders
{
    public class FolderFacade : IFolderFacade
    {
        private readonly IAmazonS3 _s3Client;
        private readonly IConfiguration _configuration;
        private readonly IFolderRepository _folderRepository;

        public FolderFacade(IAmazonS3 s3Client, IConfiguration configuration, IFolderRepository folderRepository)
        {
            _s3Client = s3Client;
            _configuration = configuration;
            _folderRepository = folderRepository;
        }

        public IEnumerable<FolderDTO> GetUserFolders(int userId)
        {
            var folders = _folderRepository.GetUserFolders(userId);

            folders
                .ToList()
                .ForEach(f => f.TotalFileSize = _folderRepository.GetTotalFileSizeInFolder(f.Id));

            return folders;
        }

        public async Task CreateFolder(int userId, FolderDTO folder)
        {
            var bucketName = _configuration["AWSBucketName"];
            var folderName = $"{userId}/{folder.Name}/";

            PutObjectRequest request = new PutObjectRequest
            {
                BucketName = bucketName,
                Key = folderName,
                ContentBody = ""
            };

            await _s3Client.PutObjectAsync(request);

            _folderRepository.CreateFolder(folder);
        }
    }
}
