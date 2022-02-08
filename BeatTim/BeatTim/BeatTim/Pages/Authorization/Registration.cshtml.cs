using System;
using System.Threading.Tasks;
using BeatTim.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeatTim.Pages
{
	public class Registration : AuthorizationPage
	{
		public static readonly string PathToPage = $"/Authorization/{nameof(Registration)}";
		[BindProperty] public RegistrationDto RegistrationDto { get; set; }

		public Registration(AuthorizationService authorizationService): base(authorizationService)
		{
		}

		public async Task<IActionResult> OnPost()
		{
			if (!ModelState.IsValid)
				return Page();

			if (await AuthorizationService.IsLoginExistsInDbAsync(RegistrationDto.Login))
			{
				ModelState.AddModelError($"{nameof(RegistrationDto)}",
					"Пользователь с таким логином уже существует");
				return Page();
			}

			var clientId = await AuthorizationService.AddNewUserAsync(RegistrationDto);

			HttpContext.Session.SetString("id", $"{clientId.Value}");
			if (RegistrationDto.RememberMe == "true")
				await AddUniqueIdentifier(clientId.Value);

			return RedirectToPage(UserAccount.PathToPage, new {id = clientId.Value});
		}
	}
}