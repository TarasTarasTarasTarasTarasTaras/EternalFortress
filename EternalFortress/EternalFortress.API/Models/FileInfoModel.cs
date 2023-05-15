namespace EternalFortress.API.Models
{
    public class FileInfoModel
    {
        public string? Name { get; set; }

        public decimal? Size { get; set; }

        public DateTime? UploadDate { get; set; }

        public int UserId { get; set; }

        public int FolderId { get; set; }
    }
}
