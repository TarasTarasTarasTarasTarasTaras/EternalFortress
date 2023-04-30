using EternalFortress.Entities.DTOs;

namespace EternalFortress.Data.Countries
{
    public interface ICountryRepository
    {
        IEnumerable<CountryDTO> GetAllCountries();
    }
}