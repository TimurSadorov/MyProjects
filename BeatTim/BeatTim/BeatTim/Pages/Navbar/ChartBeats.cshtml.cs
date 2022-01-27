using System;
using System.Collections.Generic;
using System.Text.Json;
using BeatTim.Services;
using BeatTim.Services.DTO.OutputDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BeatTim.Pages.Navbar
{
	public class ChartBeats : PageModel
	{
		public static readonly string PathToPage = $"/Navbar/{nameof(ChartBeats)}";
		private readonly BeatService _beatService;
		public IEnumerable<PublicBeatDto> TopBeats { get; private set; }
		public HashSet<int> BeatsInCart { get; private set; }
		public bool IsRegistered { get; private set; }
		public int CurrentClientId { get; private set; }
		public int CurrentAmountBeats { get; private set; }

		public ChartBeats(BeatService beatService)
		{
			_beatService = beatService;
		}

		public void OnGet()
		{
		}

		public IActionResult OnPostDownloadMoreBeats(int currentAmountBeats, int amountOutputBeats)
		{
			IsRegistered = int.TryParse(HttpContext.Items[nameof(UserToken)]?.ToString(), out var clientId);
			CurrentClientId = clientId;
			TopBeats = _beatService.GetTopBitsForPeriod(currentAmountBeats, amountOutputBeats, new TimeSpan(30, 0, 0, 0));
			if (HttpContext.Session.TryGetValue("listBeats", out var serializedBeats))
				BeatsInCart = JsonSerializer.Deserialize<HashSet<int>>(serializedBeats);
			CurrentAmountBeats = currentAmountBeats;
			return Partial("BeatsForChart", this);
		}
	}
}