using BeatTim.Models;

namespace BeatTim.Services.DTO.OutputDTO
{
	public class GenreDto
	{
		public GenreDto(Genre genre)
		{
			Name = genre.GenreName;
		}
		
		public string Name { get; set; } 
	}
}