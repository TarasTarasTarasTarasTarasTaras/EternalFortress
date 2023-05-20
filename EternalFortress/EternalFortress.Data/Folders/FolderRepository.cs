using AutoMapper;
using EternalFortress.Data.EF.Context;
using EternalFortress.Data.EF.Entities;
using EternalFortress.Entities.DTOs;

namespace EternalFortress.Data.Folders
{
    public class FolderRepository : IFolderRepository
    {
        private readonly IMapper _mapper;
        private readonly EternalFortressContext Context;

        public FolderRepository(IMapper mapper, EternalFortressContext context)
        {
            _mapper = mapper;
            Context = context;
        }

        public IEnumerable<FolderDTO> GetUserFolders(int userId)
        {
            var folders = Context
                .Folder
                .Where(f => f.UserId == userId);

            return _mapper.Map<IEnumerable<FolderDTO>>(folders);
        }

        public void CreateFolder(FolderDTO folder)
        {
            var entity = _mapper.Map<Folder>(folder);

            Context.Folder.Add(entity);
            Context.SaveChanges();
        }

        public decimal GetTotalFileSizeInFolder(int folderId)
        {
            var totalFileSize = Context
                .FileInfo
                .Where(f => f.FolderId == folderId)
                .Sum(f => f.Size);

            if (totalFileSize == null) return 0;

            return totalFileSize.Value;
        }

        public string GetFolderName(int folderId)
        {
            var name = Context
                .Folder
                .FirstOrDefault(f => f.Id == folderId)
                ?.Name;

            return name;
        }
    }
}
