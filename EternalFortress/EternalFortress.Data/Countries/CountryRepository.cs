using AutoMapper;
using EternalFortress.Data.EF.Context;
using EternalFortress.Entities.DTOs;

namespace EternalFortress.Data.Countries
{
    public class CountryRepository : ICountryRepository
    {
        private readonly IMapper _mapper;
        private readonly EternalFortressContext Context;

        public CountryRepository(EternalFortressContext context, IMapper mapper)
        {
            _mapper = mapper;
            Context = context;
        }

        public IEnumerable<CountryDTO> GetAllCountries()
        {
            var countries = Context.Countries;

            return _mapper.Map<IEnumerable<CountryDTO>>(countries);
        }
    }
}
