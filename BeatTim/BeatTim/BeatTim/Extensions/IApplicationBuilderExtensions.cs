using Microsoft.AspNetCore.Builder;

namespace BeatTim.Extensions
{
	public static class IApplicationBuilderExtensions
	{
		public static IApplicationBuilder UseUserToken(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<UserToken>();
		}
	
		public static IApplicationBuilder UseAdmin(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<Admin>();
		}
	}
}