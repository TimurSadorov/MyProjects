using System.Text;
using System.Threading.Tasks;
using BeatTim.Services;
using Microsoft.AspNetCore.Http;

namespace BeatTim
{
	public class UserToken
	{
		private readonly RequestDelegate _next;

		public UserToken(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context, AuthorizationService authorizationService)
		{
			context.Items[nameof(UserToken)] = context.Session.TryGetValue("id", out var value) ?
				int.Parse(Encoding.UTF8.GetString(value)) :
				context.Request.Cookies.TryGetValue("ui", out var val) ?
					(await authorizationService.GetClientIdByAccessTokenValueAsync(val))?.Value :
					null;
			await _next.Invoke(context);
		}
	}
}