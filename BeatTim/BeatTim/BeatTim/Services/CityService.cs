using System.Collections.Generic;
using BeatTim.Models;
using BeatTim.Repositories;

namespace BeatTim.Services
{
    public class CityService
    {
        private readonly ICityRepository _cityRepository;

        public CityService(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public IEnumerable<City> GetAllCities()
        {
            return _cityRepository.GetAll();
        }
    }
}