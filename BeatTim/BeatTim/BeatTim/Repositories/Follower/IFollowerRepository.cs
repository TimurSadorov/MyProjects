using System.Collections.Generic;
using System.Threading.Tasks;
using BeatTim.Models;

namespace BeatTim.Repositories
{
	public interface IFollowerRepository: IRepository<Follower>
	{
		public Task<Follower> GetBySubscriberIdAndUserId(int userId, int subscriberId);
		public IEnumerable<Follower> GetAllSubscriptionsWithProfile(int userId);
	}
}