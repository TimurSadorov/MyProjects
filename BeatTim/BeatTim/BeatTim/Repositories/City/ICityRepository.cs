using System.Collections.Generic;
using BeatTim.Models;

namespace BeatTim.Repositories
{
    public interface ICityRepository: IRepository<City>
    {
        public IEnumerable<City> GetAll();
    }
}