using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeatTim.Models;
using Microsoft.EntityFrameworkCore;

namespace BeatTim.Repositories
{
    public class UserProfileRepository: Repository<UserProfile>, IUserProfileRepository
    {
        public UserProfileRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }
        
        public async Task<UserProfile> GetAsync(int clientId)
        {
            return await DbContext.UserProfiles
                .Where(u => u.ClientId == clientId)
                .Include(u => u.Contact)
                .FirstOrDefaultAsync();
        }
        
        public IEnumerable<UserProfile> GetUsersByInitialCoincidenceNickname(string nickname)
        {
            return DbContext.UserProfiles
                .Where(u => u.Nickname.ToLower().StartsWith(nickname))
                .ToList();
        }
    }
}