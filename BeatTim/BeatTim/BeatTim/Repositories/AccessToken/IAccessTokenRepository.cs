using System.Threading.Tasks;
using BeatTim.Models;

namespace BeatTim.Repositories
{
    public interface IAccessTokenRepository: IRepository<AccessToken>
    {
        public Task<AccessToken> GetAsync(string tokenValue);
    }
}