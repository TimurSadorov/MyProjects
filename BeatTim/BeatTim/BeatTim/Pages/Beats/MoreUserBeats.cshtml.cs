using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using BeatTim.DTO;
using BeatTim.Services;
using BeatTim.Services.DTO.OutputDTO;
using BeatTim.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BeatTim.Pages.Beats
{
	public class MoreUserBeats : PageModel
	{
		public static readonly string PathToPage = $"/Beats/{nameof(MoreUserBeats)}";
		public IEnumerable<PublicBeatDto> PublicBeats { get; private set; }
		public UserProfileDto UserProfile { get; private set; }
		private readonly BeatService _beatService;
		private readonly AccountService _accountService;
		public bool IsGuest { get; private set; }
		public HashSet<int> BeatsInCart { get; private set; }

		public MoreUserBeats(BeatService beatService, AccountService accountService)
		{
			_beatService = beatService;
			_accountService = accountService;
		}

		public async Task OnGet(int id)
		{
			if(int.TryParse(HttpContext.Items[nameof(UserToken)]?.ToString(), out var clientId))
				IsGuest = id != clientId;
			else
				IsGuest = true;
			PublicBeats = _beatService.GetAllClientPublicBeats(id);
			UserProfile = await _accountService.GetUserProfileAsync(id);
			if (HttpContext.Session.TryGetValue("listBeats", out var serializedBeats))
				 BeatsInCart = JsonSerializer.Deserialize<HashSet<int>>(serializedBeats);
		}

		public async Task<JsonResult> OnPostAddBeatToCart(int beatId)
		{
			if (!int.TryParse(HttpContext.Items[nameof(UserToken)]?.ToString(), out var clientId))
				return new JsonResult(new Result<string>(false,
					"Покупать биты могут только авторизованные пользователи"));
			if(!await _beatService.IsBeatExistedAsync(beatId))
				return new JsonResult(new Result<string>(false,
					"Данного бита не существует, попробуйте перезагрузить страницу"));

			if (HttpContext.Session.TryGetValue("listBeats", out var serializedBeats))
			{
				var beats = JsonSerializer.Deserialize<HashSet<int>>(serializedBeats);
				beats!.Add(beatId);
				HttpContext.Session.SetString("listBeats", JsonSerializer.Serialize(beats));
			}
			else
				HttpContext.Session.SetString("listBeats", JsonSerializer.Serialize(new HashSet<int> {beatId}));
			return new JsonResult(new Result<string>(true,
				"Бит добавлен в корзину"));
		}
		
		public async Task<JsonResult> OnPostRemoveBeatToCart(int beatId)
		{
			if (!int.TryParse(HttpContext.Items[nameof(UserToken)]?.ToString(), out var clientId))
				return new JsonResult(new Result<string>(false,
					"Данное действие доступно только для авторизованных пользователей"));
			if(!await _beatService.IsBeatExistedAsync(beatId))
				return new JsonResult(new Result<string>(false,
					"Данного бита не существует, попробуйте перезагрузить страницу"));

			if (HttpContext.Session.TryGetValue("listBeats", out var serializedBeats))
			{
				var beats = JsonSerializer.Deserialize<HashSet<int>>(serializedBeats);
				beats!.Remove(beatId);
				HttpContext.Session.SetString("listBeats", JsonSerializer.Serialize(beats));
			}
			return new JsonResult(new Result<string>(true, ""));
		}
	}
}