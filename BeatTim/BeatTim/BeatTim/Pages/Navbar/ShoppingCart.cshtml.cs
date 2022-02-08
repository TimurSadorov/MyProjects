using System.Collections.Generic;
using System.Text.Json;
using BeatTim.Services;
using BeatTim.Services.DTO.OutputDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BeatTim.Pages.Navbar
{
	public class ShoppingCart : PageModel
	{
		public static readonly string PathToPage = $"/Navbar/{nameof(ShoppingCart)}";
		private readonly BeatService _beatService;
		public IEnumerable<PublicBeatDto> Beats { get; private set; }

		public ShoppingCart(BeatService beatService)
		{
			_beatService = beatService;
		}

		public IActionResult OnGet()
		{
			if (!int.TryParse(HttpContext.Items[nameof(UserToken)]?.ToString(), out var clientId))
				return RedirectToPage(Entry.PathToPage);
			HashSet<int> beatsInCart;
			if (HttpContext.Session.TryGetValue("listBeats", out var serializedBeats))
				beatsInCart = JsonSerializer.Deserialize<HashSet<int>>(serializedBeats);
			else
				beatsInCart = new HashSet<int>();
			Beats = _beatService.GetBeatsById(beatsInCart);
			return Page();
		}
	}
}