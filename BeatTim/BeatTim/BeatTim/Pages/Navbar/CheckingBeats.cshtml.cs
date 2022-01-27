using System.Collections.Generic;
using System.Threading.Tasks;
using BeatTim.Services;
using BeatTim.Services.DTO.OutputDTO;
using BeatTim.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BeatTim.Pages.Navbar
{
	public class CheckingBeats : PageModel
	{
		public static readonly string PathToPage = $"/Navbar/{nameof(CheckingBeats)}";
		private readonly BeatService _beatService;

		public CheckingBeats(BeatService beatService)
		{
			_beatService = beatService;
		}

		public IActionResult OnGet()
		{
			if (HttpContext.Items[nameof(Admin)] is null)
				return RedirectToPage(Entry.PathToPage);
			return Page();
		}

		public IActionResult OnPostDownloadCheckedBeats(int currentAmountBeats, int amountOutputBeats)
		{
			var beats = _beatService.GetUnverifiedBeatsOrderByDatePublication(currentAmountBeats, amountOutputBeats);
			return Partial("BeatsContent", beats);
		}
		
		public async Task<JsonResult> OnPostSetVerificationStatus(int beatId, bool isApproved)
		{
			var message = isApproved ? "принят" : "отклонен";
			if (await _beatService.SetVerificationStatus(beatId, isApproved))
				return new JsonResult(new Result<string>(true, $"Бит успешно {message}"));
			return new JsonResult(new Result<string>(false, "Бит уже был проверен"));
		}
	}
}