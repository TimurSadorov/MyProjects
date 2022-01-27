using System.Threading.Tasks;
using BeatTim.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeatTim.Pages
{
	public class Entry : AuthorizationPage
	{
		public static readonly string PathToPage = $"/Authorization/{nameof(Entry)}";
		[BindProperty] public AuthorizationDto AuthorizationDto { get; set; }

		public Entry(AuthorizationService authorizationService): base(authorizationService)
		{
		}

		public async Task<IActionResult> OnPost()
		{
			if (!ModelState.IsValid)
				return Page();

			var clientId =
				await AuthorizationService.GetClientIdByLoginAndPasswordAsync(AuthorizationDto.Login, AuthorizationDto.Password);

			if (clientId is null)
			{
				ModelState.AddModelError(nameof(AuthorizationDto), "Неверный логин или пароль");
				return Page();
			}

			HttpContext.Session.SetString("id", $"{clientId.Value}");
			if (AuthorizationDto.RememberMe == "true")
				await AddUniqueIdentifier(clientId.Value);

			return RedirectToPage(UserAccount.PathToPage, new {id = clientId.Value});
		}
	}
}