using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeatTim.Models;
using BeatTim.Repositories;
using BeatTim.Services.DTO.OutputDTO;

namespace BeatTim.Services
{
	public class SubscriptionService
	{
		private readonly IClientRepository _clientRepository;
		private readonly IFollowerRepository _followerRepository;
		private readonly IBeatRepository _beatRepository;

		public SubscriptionService(IClientRepository clientRepository,
			IFollowerRepository followerRepository,
			IBeatRepository beatRepository)
		{
			_clientRepository = clientRepository;
			_followerRepository = followerRepository;
			_beatRepository = beatRepository;
		}

		public IEnumerable<PublicUserProfileDto> GetSubscriptionsOrderInAlphabeticalOrder(int userId,
			int amountSkip,
			int amountTake)
		{
			var subscriptions = _followerRepository.GetAllSubscriptionsWithProfile(userId);
			var allNumberAuditionsUsers = _beatRepository
				.GetSumNumberAuditions(subscriptions
					.Select(f => f.UserId)
					.ToHashSet());
			foreach (var subscription in subscriptions)
				if (!allNumberAuditionsUsers.ContainsKey(subscription.UserId))
					allNumberAuditionsUsers[subscription.UserId] = 0;
			var allNumberAuditionsUsersOrdered = allNumberAuditionsUsers
				.OrderByDescending(x => x.Value)
				.Skip(amountSkip)
				.Take(amountTake)
				.ToDictionary(p => p.Key, p=> p.Value);
			return subscriptions
				.Where(s => allNumberAuditionsUsersOrdered.ContainsKey(s.UserId))
				.Select(s => new PublicUserProfileDto(s.UserId, s.User.UserProfile.UserPhotoLink,
					s.User.UserProfile.Nickname,
					allNumberAuditionsUsersOrdered[s.UserId]));
		}

		public async Task<bool> TrySubscribe(int subscriberId, int userId)
		{
			if (!await UserIsExists(userId))
				return false;

			var follower = await _followerRepository.GetBySubscriberIdAndUserId(userId, subscriberId);
			if (follower is not null)
				return false;

			var newFollower = new Follower {SubscriberId = subscriberId, UserId = userId};
			await _followerRepository.AddAsync(newFollower);
			return true;
		}

		public async Task<bool> TryUnsubscribe(int subscriberId, int userId)
		{
			if (!await UserIsExists(userId))
				return false;

			var follower = await _followerRepository.GetBySubscriberIdAndUserId(userId, subscriberId);
			if (follower is null)
				return false;

			await _followerRepository.DeleteAsync(follower);
			return true;
		}

		private async Task<bool> UserIsExists(int userId) => await _clientRepository.GetAsync(userId) is not null;

		public async Task<bool> IsSubscriber(int subscriberId, int userId)
		{
			return await _followerRepository.GetBySubscriberIdAndUserId(userId, subscriberId) is not null;
		}
	}
}