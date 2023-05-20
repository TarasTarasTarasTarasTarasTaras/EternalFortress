using Amazon.S3;
using Amazon.S3.Transfer;
using EternalFortress.Business.Folders;
using EternalFortress.Data.Files;
using EternalFortress.Entities.DTOs;
using Microsoft.Extensions.Configuration;

namespace EternalFortress.Business.Files
{
    public class FileFacade : IFileFacade
    {
        private readonly IAmazonS3 _s3Client;
        private readonly IFolderFacade _folderFacade;
        private readonly IConfiguration _configuration;
        private readonly IFileRepository _fileRepository;

        public FileFacade(
            IAmazonS3 s3Client,
            IFolderFacade folderFacade,
            IConfiguration configuration,
            IFileRepository fileRepository)
        {
            _s3Client = s3Client;
            _folderFacade = folderFacade;
            _configuration = configuration;
            _fileRepository = fileRepository;
        }

        public IEnumerable<FileInfoDTO> GetUserFiles(int userId, int folderId)
        {
            var files = _fileRepository.GetUserFiles(userId, folderId);
            return files;
        }

        public int SaveFileInfo(FileInfoDTO fileInfo)
        {
            var fileId = _fileRepository.SaveFileInfo(fileInfo);
            return fileId;
        }

        public async Task UploadChunkToS3(int userId, ChunkDTO dto)
        {
            var folderName = _folderFacade.GetFolderName(dto.FolderId);

            var chunkName = $"{dto.ChunkIndex:0000000000}.chunk";
            var path = $"{userId}/{folderName}/{dto.FileId}/{chunkName}";

            await using (var stream = new MemoryStream())
            {
                await dto.Chunk!.CopyToAsync(stream);

                var uploadRequest = new TransferUtilityUploadRequest
                {
                    InputStream = stream,
                    Key = path,
                    BucketName = $"{_configuration["AWSBucketName"]}",
                    ContentType = dto.Chunk.ContentType
                };

                var fileTransferUtility = new TransferUtility(_s3Client);

                await fileTransferUtility.UploadAsync(uploadRequest);
            }
        }
    }
}
