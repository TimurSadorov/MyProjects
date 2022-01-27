using System.Threading.Tasks;
using BeatTim.Models;

namespace BeatTim.Repositories
{
    public interface ILoginDetailRepository: IRepository<LoginDetails>
    {
        public Task<LoginDetails> GetAsync(string login);
    }
}