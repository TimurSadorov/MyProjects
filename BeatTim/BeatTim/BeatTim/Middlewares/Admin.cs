using System.Threading.Tasks;
using BeatTim.Services;
using Microsoft.AspNetCore.Http;

namespace BeatTim
{
	public class Admin
	{
		private readonly RequestDelegate _next;

		public Admin(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context, ClientService clientService)
		{
			int.TryParse(context.Items[nameof(UserToken)]?.ToString(), out var clientId);
			context.Items[nameof(Admin)] = await clientService.IsAdmin(clientId) ? "isAdmin" : null;
			await _next.Invoke(context);
		}
	}
}