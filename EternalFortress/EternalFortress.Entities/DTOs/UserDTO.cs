using System.Diagnostics.Metrics;

namespace EternalFortress.Entities.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }


        public int CountryId { get; set; }

        public CountryDTO? Country { get; set; }
    }
}
