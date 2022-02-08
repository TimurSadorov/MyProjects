using System.Collections.Generic;
using System.Threading.Tasks;
using BeatTim.Services;
using BeatTim.Services.OutputDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BeatTim.Pages.Beats
{
	public class EditingBeats : PageModel
	{
		public static readonly string PathToPage = $"/Beats/{nameof(EditingBeats)}";
		private readonly BeatService _beatService;
		public IEnumerable<PersonalBeatsDto> Beats;

		public EditingBeats(BeatService beatService)
		{
			_beatService = beatService;
		}

		public IActionResult OnGet()
		{
			if (!int.TryParse(HttpContext.Items[nameof(UserToken)]?.ToString(), out var clientId))
				return RedirectToPage(Entry.PathToPage);

			Beats = _beatService.GetAllClientPersonalBeats(clientId);

			return Page();
		}

		public async Task<JsonResult> OnPostDeleteBeat(int id)
		{
			if (!int.TryParse(HttpContext.Items[nameof(UserToken)]?.ToString(), out var clientId))
				return new JsonResult(false);

			return new JsonResult(await _beatService.TryDeleteBeatAsync(id, clientId));
		}

		public async Task<JsonResult> OnPostSetPrice(int beatId, int newPrice)
		{
			if (!int.TryParse(HttpContext.Items[nameof(UserToken)]?.ToString(), out var clientId))
				return new JsonResult(false);

			return new JsonResult(await _beatService.TryUpdatePriceAsync(beatId, clientId, newPrice));
		}
		
		public async Task<JsonResult> OnPostSetTitle(int beatId, string newTitle)
		{
			if (!int.TryParse(HttpContext.Items[nameof(UserToken)]?.ToString(), out var clientId))
				return new JsonResult(false);

			return new JsonResult(await _beatService.TryUpdateTitleAsync(beatId, clientId, newTitle));
		}
	}
}