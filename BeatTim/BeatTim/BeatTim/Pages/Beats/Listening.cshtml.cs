using System.Threading.Tasks;
using BeatTim.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BeatTim.Pages.Beats
{
	public class Listening : PageModel
	{
		private readonly BeatService _beatService;

		public Listening(BeatService beatService)
		{
			_beatService = beatService;
		}

		public async Task OnPostAddAuditions(int beatId)
		{
			await _beatService.AddOneListeningBeat(beatId);
		}
	}
}