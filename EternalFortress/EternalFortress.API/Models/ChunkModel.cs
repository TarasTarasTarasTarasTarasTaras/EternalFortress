namespace EternalFortress.API.Models
{
    public class ChunkModel
    {
        public IFormFile? Chunk { get; set; }

        public int FileId { get; set; }

        public int FolderId { get; set; }

        public int ChunkIndex { get; set; }
    }
}
