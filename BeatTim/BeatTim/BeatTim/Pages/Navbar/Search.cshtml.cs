using System.Collections.Generic;
using System.Linq;
using BeatTim.Services;
using BeatTim.Services.DTO.OutputDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BeatTim.Pages.Navbar
{
	public class Search : PageModel
	{
		public static readonly string PathToPage = $"/Navbar/{nameof(Search)}";
		public List<PublicBeatDto> Beats { get; private set; }
		public List<PublicUserProfileDto> Beatmakers { get; private set; }
		private readonly BeatService _beatService;
		private readonly AccountService _accountService;

		public Search(BeatService beatService, AccountService accountService)
		{
			_beatService = beatService;
			_accountService = accountService;
		}
		
		public void OnGet()
		{
			
		}

		public IActionResult OnPostSearchBeatsAndBeatmakers(string searchValue, bool searchBeats, bool searchBeatmakers)
		{
			if (searchBeats)
				Beats = _beatService
					.GetBeatsByInitialCoincidenceTitle(searchValue)
					.OrderBy(b => b.Title)
					.ToList();
			if (searchBeatmakers)
				Beatmakers = _accountService
					.GetUsersByInitialCoincidenceNickname(searchValue)
					.OrderBy(u => u.UserNickname)
					.ToList();
			Beats ??= new List<PublicBeatDto>();
			Beatmakers ??= new List<PublicUserProfileDto>();
			return Partial("SearchResult", this);
		}
	}
}