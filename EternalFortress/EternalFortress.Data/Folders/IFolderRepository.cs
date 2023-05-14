﻿using EternalFortress.Entities.DTOs;

namespace EternalFortress.Data.Folders
{
    public interface IFolderRepository
    {
        void CreateFolder(FolderDTO folder);
        IEnumerable<FolderDTO> GetUserFolders(int userId);
    }
}