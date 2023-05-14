﻿using AutoMapper;
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
    }
}
