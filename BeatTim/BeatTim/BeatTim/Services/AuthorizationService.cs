using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using BeatTim.DTO;
using BeatTim.Models;
using BeatTim.Repositories;
using BeatTim.Services.DTOOutput;

namespace BeatTim.Services
{
	public class AuthorizationService
	{
		private static readonly Cryptographer Cryptographer = new(new SHA512Managed(), new Random());
		private readonly IClientRepository _clientRepository;
		private readonly ILoginDetailRepository _loginDetailRepository;
		private readonly IUserProfileRepository _userProfileRepository;
		private readonly IAccessTokenRepository _accessTokenRepository;

		public AuthorizationService(IClientRepository clientRepository,
			ILoginDetailRepository loginDetailRepository,
			IUserProfileRepository userProfileRepository,
			IAccessTokenRepository accessTokenRepository)
		{
			_clientRepository = clientRepository;
			_loginDetailRepository = loginDetailRepository;
			_userProfileRepository = userProfileRepository;
			_accessTokenRepository = accessTokenRepository;
		}

		public async Task<AccessTokenDto> AddAccessTokenAsync(int clientId)
		{
			var accessToken = new AccessToken {ClientId = clientId, Value = Guid.NewGuid().ToString()};
			await _accessTokenRepository.AddAsync(accessToken);
			return new AccessTokenDto(accessToken.ClientId, accessToken.Value);
		}

		public async Task<IdEntity> GetClientIdByAccessTokenValueAsync(string value)
		{
			var accessToken = await _accessTokenRepository.GetAsync(value);
			return accessToken is null ? null : new IdEntity(accessToken.ClientId);
		}

		public async Task<bool> IsLoginExistsInDbAsync(string login)
		{
			return await _loginDetailRepository.GetAsync(login) is not null;
		}

		public async Task<IdEntity> AddNewUserAsync(RegistrationDto registrationDto)
		{
			var client = new Client {ClientRole = ClientRole.User};
			await _clientRepository.AddAsync(client);
			var userProfile = new UserProfile {Nickname = registrationDto.Nickname, Client = client};
			await _userProfileRepository.AddAsync(userProfile);
			var loginDetails = CreateLoginDetails(registrationDto.Login, registrationDto.Password, client);
			await _loginDetailRepository.AddAsync(loginDetails);
			return new IdEntity(client.ClientId);
		}

		public async Task<IdEntity> GetClientIdByLoginAndPasswordAsync(string login, string password)
		{
			var loginDetails = await _loginDetailRepository.GetAsync(login);
			return loginDetails is not null && loginDetails.HashedPassword ==
			       Cryptographer.GetHashedPasswordWithSalt(password, loginDetails.Salt) ?
				new IdEntity(loginDetails.ClientId) :
				null;
		}

		private static LoginDetails CreateLoginDetails(string login, string password, Client client)
		{
			var hashedPassword = Cryptographer.GetHashedPasswordWithGeneratedSalt(password);
			return new LoginDetails
			{
				Client = client, Login = login, HashedPassword = hashedPassword.HashWithSalt,
				Salt = hashedPassword.Salt
			};
		}
	}
}