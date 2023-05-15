namespace EternalFortress.Entities.DTOs
{
    public class FolderDTO
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string? Name { get; set; }

        public decimal TotalFileSize { get; set; }
    }
}
