using AutoMapper;
using EternalFortress.Data.EF.Context;
using EternalFortress.Entities.DTOs;

namespace EternalFortress.Data.Files
{
    public class FileRepository : IFileRepository
    {
        private readonly IMapper _mapper;
        private readonly EternalFortressContext Context;

        public FileRepository(IMapper mapper, EternalFortressContext context)
        {
            _mapper = mapper;
            Context = context;
        }

        public IEnumerable<FileInfoDTO> GetUserFiles(int userId, int folderId)
        {
            var files = Context
                .FileInfo
                .Where(f => f.UserId == userId && f.FolderId == folderId);

            return _mapper.Map<IEnumerable<FileInfoDTO>>(files);
        }

        public int SaveFileInfo(FileInfoDTO file)
        {
            var entity = _mapper.Map<EF.Entities.FileInfo>(file);

            Context.FileInfo.Add(entity);
            Context.SaveChanges();

            return entity.Id;
        }
    }
}
