using System.Collections.Generic;
using BeatTim.Services;
using BeatTim.Services.DTO.OutputDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BeatTim.Pages.Navbar
{
	public class Subscriptions : PageModel
	{
		public static readonly string PathToPage = $"/Navbar/{nameof(Subscriptions)}";
		private readonly SubscriptionService _subscriptionService;
		public IEnumerable<PublicUserProfileDto> AllSubscriptions { get; private set; }

		public Subscriptions(SubscriptionService subscriptionService)
		{
			_subscriptionService = subscriptionService;
		}


		public IActionResult OnGet()
		{
			if (!int.TryParse(HttpContext.Items[nameof(UserToken)]?.ToString(), out var clientId))
				return RedirectToPage(Entry.PathToPage);
			return Page();
		}

		public IActionResult OnPostGetMoreSubscriptions(int currentAmountSubscriptions, int amountOutputSubscriptions)
		{
			if (!int.TryParse(HttpContext.Items[nameof(UserToken)]?.ToString(), out var clientId))
				return RedirectToPage(Entry.PathToPage);

			AllSubscriptions = _subscriptionService.GetSubscriptionsOrderInAlphabeticalOrder(clientId, currentAmountSubscriptions, amountOutputSubscriptions);
			return Partial("Content", this);
		}
	}
}