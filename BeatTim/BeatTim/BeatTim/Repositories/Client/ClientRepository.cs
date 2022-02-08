using System.Threading.Tasks;
using BeatTim.Models;

namespace BeatTim.Repositories
{
    public class ClientRepository: Repository<Client>, IClientRepository
    {
        public ClientRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }
    }
}