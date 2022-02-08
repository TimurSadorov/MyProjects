using System.Threading.Tasks;
using BeatTim.Services;
using BeatTim.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BeatTim.Pages.Navbar
{
	public class CheckingComments : PageModel
	{
		public static readonly string PathToPage = $"/Navbar/{nameof(CheckingComments)}";
		private readonly AccountService _accountService;

		public CheckingComments(AccountService accountService)
		{
			_accountService = accountService;
		}

		public IActionResult OnGet()
		{
			if (HttpContext.Items[nameof(Admin)] is null)
				return RedirectToPage(Entry.PathToPage);
			return Page();
		}

		public IActionResult OnPostDownloadCheckedComments(int currentAmountComments, int amountOutputComments)
		{
			var comments =
				_accountService.GetPendingCommentsOrderByDatePublication(currentAmountComments, amountOutputComments);
			return Partial("CommentsContent", comments);
		}
		
		public async Task<JsonResult> OnPostSetVerificationStatus(int commentId, bool isApproved)
		{
			var message = isApproved ? "принят" : "удален";
			if (await _accountService.SetVerificationStatusComments(commentId, isApproved))
				return new JsonResult(new Result<string>(true, $"Комментарий {message}"));
			return new JsonResult(new Result<string>(false, "Комментарий уже был проверен"));
		}
	}
}