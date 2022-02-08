using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeatTim.Models;
using Microsoft.EntityFrameworkCore;

namespace BeatTim.Repositories
{
	public class UserCommentRepository : Repository<UserComment>, IUserCommentRepository
	{
		public UserCommentRepository(ApplicationContext dbContext) : base(dbContext)
		{
		}
		
		public IEnumerable<UserComment> GetAll(int commentedId)
		{
			return DbContext.UserComments
				.Where(u => u.CommentedId == commentedId)
				.Include(comment => comment.Commentator)
				.ThenInclude(u => u.UserProfile)
				.ToList();
		}
		
		public IEnumerable<UserComment> GetSortedDate(int commentedId, int skipCount, int takeCount)
		{
			return DbContext.UserComments
				.Where(u => u.CommentedId == commentedId)
				.OrderByDescending(c => c.PublicationDate)
				.Skip(skipCount)
				.Take(takeCount)
				.Include(comment => comment.Commentator)
				.ThenInclude(u => u.UserProfile)
				.ToList();
		}

		public async Task<UserComment> GetWithUserProfileAsync(int commentId)
		{
			return await Entities
				.Where(c => c.UserCommentId == commentId)
				.Include(c => c.Commentator)
				.ThenInclude(c => c.UserProfile)
				.FirstOrDefaultAsync();
		}

		public IEnumerable<UserComment> GetPendingCommentsOrderByDatePublication(int amountSkip, int amountTake)
		{
			return Entities
				.Where(c => c.IsUnderConsideration)
				.OrderBy(c => c.PublicationDate)
				.Skip(amountSkip)
				.Take(amountTake)
				.Include(c => c.Commentator)
				.ThenInclude(c => c.UserProfile)
				.ToList();
		}
	}
}