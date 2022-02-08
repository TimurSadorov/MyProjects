using System.Threading.Tasks;
using BeatTim.Models;

namespace BeatTim.Repositories
{
    public interface IContactRepository: IRepository<Contact>
    {
        public Task<Contact> GetAsync(int contactId);
    }
}