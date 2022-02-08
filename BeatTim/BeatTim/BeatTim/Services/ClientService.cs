using System.Threading.Tasks;
using BeatTim.Models;
using BeatTim.Repositories;

namespace BeatTim.Services
{
	public class ClientService
	{
		private readonly IClientRepository _clientRepository;

		public ClientService(IClientRepository clientRepository)
		{
			_clientRepository = clientRepository;
		}

		public async Task<bool> IsAdmin(int clientId)
		{
			return (await _clientRepository.GetAsync(clientId))?.ClientRole is ClientRole.Admin;
		}
	}
}