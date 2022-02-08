using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using BeatTim.DTO.InputDTO;
using BeatTim.Models;
using BeatTim.Repositories;
using BeatTim.Services.DTO.OutputDTO;
using BeatTim.Services.OutputDTO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;

namespace BeatTim.Services
{
	public class BeatService
	{
		private readonly IBeatRepository _beatRepository;
		private readonly IGenreRepository _genreRepository;
		private static readonly string pathToCovers = "/img/user_beats_covers/";
		private static readonly string pathToAudio = "/audio/";
		private readonly IWebHostEnvironment _appEnvironment;

		public BeatService(IBeatRepository beatRepository,
			IGenreRepository genreRepository,
			IWebHostEnvironment appEnvironment)
		{
			_beatRepository = beatRepository;
			_genreRepository = genreRepository;
			_appEnvironment = appEnvironment;
		}

		public async Task<bool> SetVerificationStatus(int beatId, bool isApproved)
		{
			var beat = await _beatRepository.GetAsync(beatId);
			if (beat?.VerificationStatus is not VerificationStatus.Unverified)
				return false;

			beat.VerificationStatus = isApproved ? VerificationStatus.Approved : VerificationStatus.Refused;
			await _beatRepository.UpdateAsync(beat);
			return true;
		}

		public async Task AddOneListeningBeat(int beatId)
		{
			var beat = await _beatRepository.GetAsync(beatId);
			if(beat is null)
				return;

			beat.NumberAuditions++;
			await _beatRepository.UpdateAsync(beat);
		}

		public IEnumerable<PublicBeatDto> GetUnverifiedBeatsOrderByDatePublication(int amountSkip, int amountTake)
		{
			return _beatRepository
				.GetUnverifiedBeatsOrderByDatePublication(amountSkip, amountTake)
				.Select(b => new PublicBeatDto(b));
		}

		public IEnumerable<PublicBeatDto> GetBeatsByInitialCoincidenceTitle(string title)
		{
			return _beatRepository.GetBeatsByInitialCoincidenceTitle(title.ToLower())
				.Select(b => new PublicBeatDto(b));
		}

		public IEnumerable<PublicBeatDto> GetBeatsById(HashSet<int> ids)
		{
			return _beatRepository.GetBeatsByIds(ids)
				.Select(b => new PublicBeatDto(b));
		}

		public IEnumerable<PublicBeatDto> GetTopBitsForPeriod(int amountSkip, int amountTake, TimeSpan period)
		{
			return _beatRepository.GetTopBitsForPeriod(amountSkip, amountTake, period)
				.Select(b => new PublicBeatDto(b));
		}
		
		public Dictionary<string, IEnumerable<PublicBeatDto>> GetTopBitsForEachDirectionForPeriod(int amount, TimeSpan period)
		{
			var result = new Dictionary<string, IEnumerable<PublicBeatDto>>();
			var allGenres = _genreRepository.GetAll();

			foreach (var genre in allGenres)
				result[genre.GenreName] = _beatRepository.GetTopBitsByDirectionForPeriod(amount, genre.GenreId, period)
					.Select(b => new PublicBeatDto(b));
			result[""] = _beatRepository.GetTopBitsByDirectionForPeriod(amount, null, period).Select(b => new PublicBeatDto(b));
			return result;
		}

		public IEnumerable<PersonalBeatsDto> GetAllClientPersonalBeats(int clientId)
		{
			return _beatRepository.GetAll(clientId).Select(u => new PersonalBeatsDto(u));
		}
		
		public IEnumerable<PublicBeatDto> GetAllClientPublicBeats(int clientId)
		{
			return _beatRepository.GetAll(clientId)
				.Where(b => b.VerificationStatus == VerificationStatus.Approved)
				.Select(u => new PublicBeatDto(u));
		}


		public async Task<bool> TryDeleteBeatAsync(int beatId, int userId)
		{
			var beat = await _beatRepository.GetAsync(beatId);

			if (beat is null || beat.ClientId != userId)
				return false;

			await _beatRepository.DeleteAsync(beat);
			return true;
		}

		public async Task<bool> TryUpdatePriceAsync(int beatId, int userId, int newPrice)
		{
			if (newPrice < 0)
				return false;

			var beat = await _beatRepository.GetAsync(beatId);

			if (beat is null || beat.ClientId != userId)
				return false;

			beat.Price = newPrice;
			await _beatRepository.UpdateAsync(beat);
			return true;
		}
		
		public async Task<bool> TryUpdateTitleAsync(int beatId, int userId, string newTitle)
		{
			if (newTitle.Length is 0 or > 100)
				return false;

			var beat = await _beatRepository.GetAsync(beatId);

			if (beat is null || beat.ClientId != userId)
				return false;

			beat.Title = newTitle;
			await _beatRepository.UpdateAsync(beat);
			return true;
		}

		public async Task AddNewBeatAsync(NewBeatDto newBeat, int userId)
		{
			var beatModel = await CreateNewBeat(newBeat, userId);
			await _beatRepository.AddAsync(beatModel);
		}

		private async Task<Beat> CreateNewBeat(NewBeatDto newBeat, int userId)
		{
			var beat = new Beat
			{
				Title = newBeat.Title,
				Price = newBeat.Price,
				ClientId = userId,
				VerificationStatus = VerificationStatus.Unverified,
				PublicationDate = DateTime.Now
			};
			if (newBeat.GenreName is not null)
				beat.Genre = await _genreRepository.GetByName(newBeat.GenreName);
			if (newBeat.Cover is not null)
				beat.CoverLink = await AddCover(newBeat.Cover);
			beat.BeatLink = await AddAudioFile(newBeat.Audio);
			return beat;
		}

		private async Task<string> AddCover(Image image)
		{
			var userPhotoLink = pathToCovers +
			                    $"{GenerateRandomFileName()}.jpg";
			await image.SaveAsync(_appEnvironment.WebRootPath + userPhotoLink);
			return userPhotoLink;
		}

		private async Task<string> AddAudioFile(IFormFile audio)
		{
			var pathToFile = pathToAudio + $"{GenerateRandomFileName()}{Path.GetExtension(audio.FileName)}";

			await using var fileStream = new FileStream(_appEnvironment.WebRootPath + pathToFile, FileMode.Create);
			await audio.CopyToAsync(fileStream);

			return pathToFile;
		}

		private string GenerateRandomFileName() => Path.GetFileNameWithoutExtension(Path.GetRandomFileName());

		public async Task<bool> IsBeatExistedAsync(int beatId) =>
			await _beatRepository.GetAsync(beatId) is not null;
	}
}