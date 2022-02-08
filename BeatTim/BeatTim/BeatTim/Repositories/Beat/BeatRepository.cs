using System;
using System.Collections.Generic;
using System.Linq;
using BeatTim.Models;
using BeatTim.Services.DTO.OutputDTO;
using Microsoft.EntityFrameworkCore;

namespace BeatTim.Repositories
{
	public class BeatRepository: Repository<Beat>, IBeatRepository
	{
		public BeatRepository(ApplicationContext dbContext) : base(dbContext)
		{
		}

		public IEnumerable<Beat> GetAll(int clientId)
		{
			return Entities
				.Where(b => b.ClientId == clientId)
				.Include(b => b.Genre)
				.ToList();
		}

		public IEnumerable<Beat> GetTopBitsByDirectionForPeriod(int amount, int? genreId, TimeSpan period)
		{
			var now = DateTime.Now;
			return Entities
				.Where(b => genreId == b.GenreId && VerificationStatus.Approved == b.VerificationStatus)
				.OrderByDescending(b => b.NumberAuditions)
				.Take(amount)
				.Include(b => b.Client)
				.ThenInclude(c => c.UserProfile)
				.ToList();
		}
		
		public IEnumerable<Beat> GetTopBitsForPeriod(int amountSkip, int amountTake, TimeSpan period)
		{
			var now = DateTime.Now;
			return Entities
				.Where(b => VerificationStatus.Approved == b.VerificationStatus)
				.OrderByDescending(b => b.NumberAuditions)
				.Skip(amountSkip)
				.Take(amountTake)
				.Include(b => b.Client)
				.ThenInclude(c => c.UserProfile)
				.ToList();
		}

		public Dictionary<int, int> GetSumNumberAuditions(HashSet<int> usersId)
		{
			return Entities.Where(b => usersId.Contains(b.ClientId) && VerificationStatus.Approved == b.VerificationStatus)
				.GroupBy(b => b.ClientId)
				.Select(g => new {g.Key, Sum = g.Sum(b => b.NumberAuditions)})
				.ToDictionary(g => g.Key, g => g.Sum);
		}

		public IEnumerable<Beat> GetBeatsByIds(HashSet<int> ids)
		{
			return Entities
				.Where(b => ids.Contains(b.BeatId))
				.Include(b => b.Client)
				.ThenInclude(c => c.UserProfile)
				.ToList();
		}

		public IEnumerable<Beat> GetBeatsByInitialCoincidenceTitle(string title)
		{
			return Entities
				.Where(b => b.VerificationStatus == VerificationStatus.Approved && b.Title.ToLower().StartsWith(title))
				.Include(b => b.Client)
				.ThenInclude(c => c.UserProfile)
				.ToList();
		}

		public IEnumerable<Beat> GetUnverifiedBeatsOrderByDatePublication(int amountSkip, int amountTake)
		{
			return Entities
				.Where(b => b.VerificationStatus == VerificationStatus.Unverified)
				.OrderBy(b => b.PublicationDate)
				.Skip(amountSkip)
				.Take(amountTake)
				.Include(b => b.Client)
				.ThenInclude(c => c.UserProfile)
				.ToList();
		}
	}
}