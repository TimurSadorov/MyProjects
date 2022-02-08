using System.Collections.Generic;
using System.Linq;
using BeatTim.Repositories;
using BeatTim.Services.DTO.OutputDTO;

namespace BeatTim.Services
{
	public class GenreService
	{
		private readonly IGenreRepository _genreRepository;

		public GenreService(IGenreRepository genreRepository)
		{
			_genreRepository = genreRepository;
		}

		public IEnumerable<GenreDto> GetAllGenre()
		{
			return _genreRepository.GetAll().Select(g => new GenreDto(g));
		}
	}
}