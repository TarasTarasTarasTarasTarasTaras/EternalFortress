namespace EternalFortress.Data.EF.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }


        public int CountryId { get; set; }

        public Country? Country { get; set; }

        public IEnumerable<Folder>? Folders { get; set; }

        public IEnumerable<FileInfo>? Files { get; set; }
    }
}
