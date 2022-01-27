using System.Threading.Tasks;
using BeatTim.Models;

namespace BeatTim.Repositories
{
	public interface IGenreRepository: IRepository<Genre>
	{
		public Task<Genre> GetByName(string name);
	}
}