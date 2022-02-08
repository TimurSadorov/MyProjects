using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeatTim.Models;
using Microsoft.EntityFrameworkCore;

namespace BeatTim.Repositories
{
	public abstract class Repository<TEntity> : IRepository<TEntity>
		where TEntity : class
	{
		protected readonly ApplicationContext DbContext;
		protected readonly DbSet<TEntity> Entities;

		protected Repository(ApplicationContext dbContext)
		{
			DbContext = dbContext;
			Entities = dbContext.Set<TEntity>();
		}

		public virtual async Task AddAsync(TEntity entity)
		{
			await DbContext.AddAsync(entity);
			await DbContext.SaveChangesAsync();
		}

		public virtual async Task UpdateAsync(TEntity userProfile)
		{
			DbContext.Update(userProfile);
			await DbContext.SaveChangesAsync();
		}

		public virtual async Task DeleteAsync(TEntity entity)
		{
			DbContext.Remove(entity);
			await DbContext.SaveChangesAsync();
		}

		public virtual async Task<TEntity> GetAsync(int idEntity)
		{
			return await DbContext.FindAsync<TEntity>(idEntity);
		}
		
		public virtual IEnumerable<TEntity> GetAll()
		{
			return DbContext.Set<TEntity>().ToList();
		}
	}
}