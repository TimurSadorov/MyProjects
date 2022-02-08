using System.Collections.Generic;
using System.Linq;
using BeatTim.Models;

namespace BeatTim.Repositories
{
	public class CityRepository: Repository<City>, ICityRepository
	{
		public CityRepository(ApplicationContext dbContext) : base(dbContext)
		{
		}

		public IEnumerable<City> GetAll()
		{
			return DbContext.Cities.ToList();
		}
	}
}