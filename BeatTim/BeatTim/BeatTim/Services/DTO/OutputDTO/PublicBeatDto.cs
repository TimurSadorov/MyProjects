using BeatTim.Models;

namespace BeatTim.Services.DTO.OutputDTO
{
	public class PublicBeatDto: BeatDto
	{
		public PublicBeatDto(Beat beat) : base(beat)
		{
			UserName = beat.Client?.UserProfile.Nickname;
			UserId = beat.ClientId;
		}
		
		public string UserName { get; set; }
		public int UserId { get; set; }
	}
}