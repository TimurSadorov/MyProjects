using System.Threading.Tasks;
using BeatTim.Models;
using Microsoft.EntityFrameworkCore;

namespace BeatTim.Repositories
{
	public class GenreRepository: Repository<Genre>, IGenreRepository
	{
		public GenreRepository(ApplicationContext dbContext) : base(dbContext)
		{
		}

		public async Task<Genre> GetByName(string name)
		{
			return await DbContext.Genres.FirstOrDefaultAsync(g => g.GenreName == name);
		}
	}
}