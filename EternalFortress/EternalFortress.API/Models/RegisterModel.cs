using System.ComponentModel;

namespace EternalFortress.API.Models
{
    public class RegisterModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int CountryId { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
