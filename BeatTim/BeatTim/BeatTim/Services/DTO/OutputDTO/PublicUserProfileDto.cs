namespace BeatTim.Services.DTO.OutputDTO
{
	public class PublicUserProfileDto
	{
		public PublicUserProfileDto(int userId, string userPhotoLink, string userNickname, int totalNumberAuditions)
		{
			UserId = userId;
			UserPhotoLink = userPhotoLink;
			UserNickname = userNickname;
			TotalNumberAuditions = totalNumberAuditions;
		}

		public int UserId { get; set; }
		public string UserPhotoLink { get; set; }
		public string UserNickname { get; set; }
		public int TotalNumberAuditions { get; set; }
	}
}