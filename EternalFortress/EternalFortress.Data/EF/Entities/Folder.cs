namespace EternalFortress.Data.EF.Entities
{
    public class Folder
    {
        public int Id { get; set; }

        public string? Name { get; set; }


        public int UserId { get; set; }

        public User? User { get; set; }

        public IEnumerable<FileInfo>? Files { get; set; }
    }
}
