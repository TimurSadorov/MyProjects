using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeatTim.DTO.InputDTO;
using BeatTim.Services;
using BeatTim.Services.DTO.OutputDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BeatTim.Pages.Beats
{
	public class AddingBeat : PageModel
	{
		public static string PathToPage = $"/Beats/{nameof(AddingBeat)}";
		public IEnumerable<GenreDto> Genres;
		private readonly BeatService _beatService;
		private readonly ImageService _imageService;
		private readonly AudioService _audioService;
		[BindProperty]
		public NewBeatDto NewBeat { get; set; }
		[BindProperty]
		public IFormFile Cover { get; set; }

		public AddingBeat(GenreService genreService, BeatService beatService, ImageService imageService, AudioService audioService)
		{
			_beatService = beatService;
			_imageService = imageService;
			_audioService = audioService;
			Genres = genreService.GetAllGenre() ?? Enumerable.Empty<GenreDto>();
		}

		public IActionResult OnGet()
		{
			return Page();
		}

		public async Task<IActionResult> OnPost()
		{
			if (!ModelState.IsValid)
			{
				ModelState.AddModelError(nameof(NewBeat), "Введены неккоректные данные");
				return Page();
			}
			if (!int.TryParse(HttpContext.Items[nameof(UserToken)]?.ToString(), out var clientId))
				return RedirectToPage(Entry.PathToPage);

			if (NewBeat.Audio is null)
			{
				ModelState.AddModelError($"{nameof(NewBeat)}.{nameof(NewBeat.Audio)}", "Необходимо загрузить бит");
				return Page();
			}

			if (!_audioService.IsCorrectAudioExtension(NewBeat.Audio.FileName))
			{
				ModelState.AddModelError($"{nameof(NewBeat)}.{nameof(NewBeat.Audio)}", "Аудио файл имеет некорректное расширение");
				return Page();
			}

			if (Cover is not null )
			{
				if (!_imageService.TryConvertToImage(Cover, out var image))
				{
					ModelState.AddModelError(nameof(Cover), "Обложка имеет некорректное расширение файла");
					return Page();
				}
				NewBeat.Cover = image;

				if (!_imageService.IsValidImageSize(NewBeat.Cover))
				{
					ModelState.AddModelError(nameof(Cover), "Обложка имеет некорректный размер");
					return Page();
				}
			}

			await _beatService.AddNewBeatAsync(NewBeat, clientId);

			return RedirectToPage(EditingBeats.PathToPage);
		}
	}
}