﻿using Amazon.S3;
using Amazon.S3.Transfer;
using EternalFortress.Business.Encryption;
using EternalFortress.Business.Folders;
using EternalFortress.Data.Files;
using EternalFortress.Entities.DTOs;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace EternalFortress.Business.Files
{
    public class FileFacade : IFileFacade
    {
        private readonly IAmazonS3 _s3Client;
        private readonly IFolderFacade _folderFacade;
        private readonly IConfiguration _configuration;
        private readonly IFileRepository _fileRepository;
        private readonly IEncryptionService _encryptionService;

        public FileFacade(
            IAmazonS3 s3Client,
            IFolderFacade folderFacade,
            IConfiguration configuration,
            IFileRepository fileRepository,
            IEncryptionService encryptionService)
        {
            _s3Client = s3Client;
            _folderFacade = folderFacade;
            _configuration = configuration;
            _fileRepository = fileRepository;
            _encryptionService = encryptionService;
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

                var data = MemoryStreamToString(stream);
                var encryptedData = _encryptionService.Encrypt(data);

                var uploadRequest = new TransferUtilityUploadRequest
                {
                    InputStream = new MemoryStream(encryptedData),
                    Key = path,
                    BucketName = $"{_configuration["AWSBucketName"]}",
                    ContentType = dto.Chunk.ContentType
                };

                var fileTransferUtility = new TransferUtility(_s3Client);

                await fileTransferUtility.UploadAsync(uploadRequest);
            }
        }

        private string MemoryStreamToString(MemoryStream memoryStream)
        {
            memoryStream.Position = 0;

            using (StreamReader reader = new StreamReader(memoryStream, Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }

        private MemoryStream StringToMemoryStream(string inputString)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(inputString);

            return new MemoryStream(bytes);
        }
    }
}
