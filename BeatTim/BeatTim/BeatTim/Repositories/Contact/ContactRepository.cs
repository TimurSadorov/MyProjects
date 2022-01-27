using System.Threading.Tasks;
using BeatTim.Models;
using Microsoft.EntityFrameworkCore;

namespace BeatTim.Repositories
{
	public class ContactRepository: Repository<Contact>, IContactRepository
	{
		public ContactRepository(ApplicationContext dbContext) : base(dbContext)
		{
		}

		public async Task<Contact> GetAsync(int contactId)
		{
			return await DbContext.Contacts.FirstOrDefaultAsync(c => c.ContactId == contactId);
		}
	}
}