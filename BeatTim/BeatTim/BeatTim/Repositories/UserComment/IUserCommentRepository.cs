using System.Collections.Generic;
using System.Threading.Tasks;
using BeatTim.Models;

namespace BeatTim.Repositories
{
    public interface IUserCommentRepository: IRepository<UserComment>
    {
        public IEnumerable<UserComment> GetAll(int commentedId);

        public IEnumerable<UserComment> GetSortedDate(int commentedId, int skipCount, int takeCount);
        public Task<UserComment> GetWithUserProfileAsync(int commentId);
        public IEnumerable<UserComment> GetPendingCommentsOrderByDatePublication(int amountSkip, int amountTake);
    }
}