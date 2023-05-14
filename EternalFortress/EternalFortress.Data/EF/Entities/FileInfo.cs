namespace EternalFortress.Data.EF.Entities
{
    public class FileInfo
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public decimal? Size { get; set; }

        public DateTime? UploadDate { get; set; }

        public int UserId { get; set; }

        public int FolderId { get; set; }

        public User? User { get; set; }

        public Folder? Folder { get; set; }
    }
}
