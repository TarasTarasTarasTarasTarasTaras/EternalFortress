using Microsoft.AspNetCore.Http;

namespace EternalFortress.Entities.DTOs
{
    public class ChunkDTO
    {
        public IFormFile? Chunk { get; set; }

        public int FileId { get; set; }

        public int FolderId { get; set; }

        public int ChunkIndex { get; set; }
    }
}
