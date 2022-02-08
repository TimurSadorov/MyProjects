using System.Threading.Tasks;
using BeatTim.Models;
using Microsoft.EntityFrameworkCore;

namespace BeatTim.Repositories
{
	public class AccessTokenRepository : Repository<AccessToken>, IAccessTokenRepository
	{
		public AccessTokenRepository(ApplicationContext dbContext) : base(dbContext)
		{
		}

		public async Task<AccessToken> GetAsync(string tokenValue)
		{
			return await DbContext.AccessTokens
				.FirstOrDefaultAsync(t => t.Value == tokenValue);
		}
	}
}