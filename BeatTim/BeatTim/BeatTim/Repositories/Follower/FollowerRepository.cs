using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeatTim.Models;
using Microsoft.EntityFrameworkCore;

namespace BeatTim.Repositories
{
	public class FollowerRepository: Repository<Follower>, IFollowerRepository
	{
		public FollowerRepository(ApplicationContext dbContext) : base(dbContext)
		{
		}

		public async Task<Follower> GetBySubscriberIdAndUserId(int userId, int subscriberId)
		{
			return await Entities
				.FirstOrDefaultAsync(f => f.UserId == userId && f.SubscriberId == subscriberId);
		}
		
		public IEnumerable<Follower> GetAllSubscriptionsWithProfile(int userId)
		{
			return Entities
				.Where(f => f.SubscriberId == userId)
				.Include(f => f.User)
				.ThenInclude(c => c.UserProfile)
				.ToList();
		}
	}
}