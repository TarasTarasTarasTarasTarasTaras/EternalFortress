namespace EternalFortress.Entities.DTOs
{
    public class FileInfoDTO
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public decimal? Size { get; set; }

        public DateTime? UploadDate { get; set; }

        public int FolderId { get; set; }

        public FolderDTO? Folder { get; set; }

        public int UserId { get; set; }
        
        public UserDTO? User { get; set; }
    }
}
