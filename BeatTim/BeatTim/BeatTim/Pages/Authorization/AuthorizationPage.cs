using System;
using System.Threading.Tasks;
using BeatTim.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BeatTim.Pages
{
	public class AuthorizationPage: PageModel
	{
		protected readonly AuthorizationService AuthorizationService;

		protected AuthorizationPage(AuthorizationService authorizationService)
		{
			AuthorizationService = authorizationService;
		}

		public IActionResult OnGet()
		{
			if (HttpContext.Items[nameof(UserToken)] is not null)
				return RedirectToPage(Index.PathToPage);

			return Page();
		}
		
		protected async Task AddUniqueIdentifier(int clientId)
		{
			var accessToken = await AuthorizationService.AddAccessTokenAsync(clientId);
			Response.Cookies.Append("ui", accessToken.Value, new CookieOptions()
			{
				MaxAge = TimeSpan.FromDays(30)
			});
		}
	}
}