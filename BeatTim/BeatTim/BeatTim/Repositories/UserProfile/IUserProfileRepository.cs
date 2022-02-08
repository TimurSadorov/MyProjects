using System.Collections.Generic;
using System.Threading.Tasks;
using BeatTim.Models;

namespace BeatTim.Repositories
{
    public interface IUserProfileRepository: IRepository<UserProfile>
    {
        public Task<UserProfile> GetAsync(int clientId);
        public IEnumerable<UserProfile> GetUsersByInitialCoincidenceNickname(string nickname);
    }
}