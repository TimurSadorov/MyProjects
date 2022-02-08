using System.Collections.Generic;
using System.Threading.Tasks;
using BeatTim.DTO;
using BeatTim.Services;
using BeatTim.Services.DTO.OutputDTO;
using BeatTim.Services.OutputDTO;
using BeatTim.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BeatTim.Pages
{
	public class UserAccount : PageModel
	{
		public static readonly string PathToPage = $"/Account/{nameof(UserAccount)}";

		public UserProfileDto UserProfile { get; private set; }
		public IEnumerable<PublicBeatDto> VerifiedBeats { get; private set; }
		private readonly AccountService _accountService;
		private readonly BeatService _beatService;
		private readonly SubscriptionService _subscriptionService;
		public bool IsGuest { get; private set; }
		public bool IsSubscriber { get; set; }

		public UserAccount(AccountService accountService,
			BeatService beatService,
			SubscriptionService subscriptionService)
		{
			_accountService = accountService;
			_beatService = beatService;
			_subscriptionService = subscriptionService;
		}

		public async Task<IActionResult> OnGet(int id)
		{
			if (int.TryParse(HttpContext.Items[nameof(UserToken)]?.ToString(), out var clientId))
			{
				IsGuest = id != clientId;
				IsSubscriber = await _subscriptionService.IsSubscriber(clientId, id);
			}
			else
			{
				IsGuest = true;
				IsSubscriber = false;
			}

			UserProfile = await _accountService.GetUserProfileAsync(id);
			if (UserProfile is null)
				return Partial("NotFoundUser");

			VerifiedBeats = _beatService.GetAllClientPublicBeats(id);
			return Page();
		}

		public IActionResult OnGetExit()
		{
			Response.Cookies.Delete("ui");
			HttpContext.Session.Clear();
			return RedirectToPage(Index.PathToPage);
		}

		public async Task<JsonResult> OnPostDeleteComment(int commentId)
		{
			if (await _accountService.TryDeleteComment(commentId))
				return new JsonResult(new Result<string>(true, "Комментарий успешно удален"));
			return new JsonResult(new Result<string>(false, "Данного комментария не существует, попробуйте перезагрузить страницу и повторить операцию"));
		}

		public IActionResult OnPostMoreComments(int currentCommentsAmount, int commentOutput)
		{
			int.TryParse(RouteData.Values["id"]!.ToString(), out var clientId);

			var nextComments = _accountService.GetSortedCommentByDate(clientId, currentCommentsAmount, commentOutput);

			return Partial("Comments", nextComments);
		}

		public async Task<JsonResult> OnPostReportComment(int commentId)
		{
			if (!int.TryParse(HttpContext.Items[nameof(UserToken)]?.ToString(), out var clientId))
				return new JsonResult(new Result<string>(false,
					"Отправлять жалобу могут только зарегистрированные пользователи"));

			if (await _accountService.TryReportCommentAsync(commentId))
				return new JsonResult(new Result<string>(true, "Жалоба успешно отправлена"));

			return new JsonResult(new Result<string>(false,
				"При отправке жалобы произошла ошибка, попробуйте перезагрузить страницу и повторить операцию"));
		}

		public async Task<IActionResult> OnPostAddComment(string commentContent)
		{
			if (!int.TryParse(HttpContext.Items[nameof(UserToken)]?.ToString(), out var commentatorId))
				return new JsonResult(new Result<string>(false,
					"Оставлять комментарии могут только зарегистрированные пользователи"));

			int.TryParse(RouteData.Values["id"]!.ToString(), out var commentedId);
			if (commentatorId == commentedId)
				return new JsonResult(new Result<string>(false,
					"Вы не можете оставлять комментарий под своей страрицой"));

			var idComment = await _accountService.TryAddCommentAsync(commentContent, commentedId, commentatorId);
			if (idComment is null)
				return new JsonResult(new Result<string>(false,
					"При отправке комментария произошла ошибка, попробуйте перезагрузить страницу и повторить операцию"));

			var comment = await _accountService.GetComment(idComment.Value);
			return Partial("Comments", new List<UserCommentDto> {comment});
		}

		public async Task<JsonResult> OnPostSubscribe()
		{
			if (!int.TryParse(HttpContext.Items[nameof(UserToken)]?.ToString(), out var subscriber))
				return new JsonResult(new Result<string>(false,
					"Для того чтобы чтобы подаисаться, вам необходимо зарегистрироваться"));

			int.TryParse(RouteData.Values["id"]!.ToString(), out var userId);
			if (subscriber == userId)
				return new JsonResult(new Result<string>(false,
					"Вы не можете подписаться на самого себя"));

			if (!await _subscriptionService.TrySubscribe(subscriber, userId))
				return new JsonResult(new Result<string>(false,
					"Во время подписки произошла ошибка, попробуйте перезагрузить страницу и повторить операцию снова"));

			return new JsonResult(new Result<string>(true,
				"Вы успешно подписаны на аккаунт"));
		}

		public async Task<JsonResult> OnPostUnsubscribe()
		{
			if (!int.TryParse(HttpContext.Items[nameof(UserToken)]?.ToString(), out var subscriber))
				return new JsonResult(new Result<string>(false,
					"Это действие доступно только зарегистрированным пользователям"));

			int.TryParse(RouteData.Values["id"]!.ToString(), out var userId);
			if (subscriber == userId)
				return new JsonResult(new Result<string>(false,
					"Вы не можете отписаться от самого себя"));


			if (!await _subscriptionService.TryUnsubscribe(subscriber, userId))
				return new JsonResult(new Result<string>(false,
					"Во время отписки произошла ошибка, попробуйте перезагрузить страницу и повторить операцию снова"));

			return new JsonResult(new Result<string>(true,
				"Вы успешно отписались от аккаунта"));
		}
	}
}