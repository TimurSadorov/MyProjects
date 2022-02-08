using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeatTim.Repositories
{
    public interface IRepository<TEntity>
    {
        public Task AddAsync(TEntity entity);
        public Task UpdateAsync(TEntity userProfile);
        public Task DeleteAsync(TEntity entity);
        public Task<TEntity> GetAsync(int idEntity);
        public IEnumerable<TEntity> GetAll();
    }
}