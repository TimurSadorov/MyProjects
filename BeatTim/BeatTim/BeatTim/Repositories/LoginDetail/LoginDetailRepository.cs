using System.Threading.Tasks;
using BeatTim.Models;
using Microsoft.EntityFrameworkCore;

namespace BeatTim.Repositories
{
	public class LoginDetailRepository : Repository<LoginDetails>, ILoginDetailRepository
	{
		public LoginDetailRepository(ApplicationContext dbContext) : base(dbContext)
		{
		}

		public async Task<LoginDetails> GetAsync(string login)
		{
			return await DbContext.LoginDetails.FirstOrDefaultAsync(details =>
				details.Login == login);
		}
	}
}