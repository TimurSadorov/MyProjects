using System;
using System.Collections.Generic;
using BeatTim.Services;
using BeatTim.Services.DTO.OutputDTO;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BeatTim.Pages
{
	public class Index : PageModel
	{
		public static readonly string PathToPage = $"/{nameof(Index)}";
		private readonly BeatService _beatService;
		public Dictionary<string, IEnumerable<PublicBeatDto>> TopBeats { get; private set; }

		public Index(BeatService beatService)
		{
			_beatService = beatService;
		}

		public void OnGet()
		{
			TopBeats = _beatService.GetTopBitsForEachDirectionForPeriod(20, new TimeSpan(30, 0, 0, 0));
		}
	}
}