using System;
using System.Collections.Generic;
using System.Linq;
using BeatTim.Models;

namespace BeatTim.Repositories
{
    public interface IBeatRepository: IRepository<Beat>
    {
        public IEnumerable<Beat> GetAll(int clientId);
        public IEnumerable<Beat> GetTopBitsByDirectionForPeriod(int amount, int? genreId, TimeSpan period);
        public IEnumerable<Beat> GetTopBitsForPeriod(int amountSkip, int amountTake, TimeSpan period);
        public Dictionary<int, int>  GetSumNumberAuditions(HashSet<int> usersId);
        public IEnumerable<Beat> GetBeatsByIds(HashSet<int> ids);
        public IEnumerable<Beat> GetBeatsByInitialCoincidenceTitle(string title);
        public IEnumerable<Beat> GetUnverifiedBeatsOrderByDatePublication(int amountSkip, int amountTake);
    }
}