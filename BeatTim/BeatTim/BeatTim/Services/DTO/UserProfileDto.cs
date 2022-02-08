using System.ComponentModel.DataAnnotations;
using BeatTim.Models;
using SixLabors.ImageSharp;

namespace BeatTim.DTO
{
	public class UserProfileDto
	{
		public UserProfileDto()
		{
		}

		public UserProfileDto(UserProfile userProfile)
		{
			Nickname = userProfile.Nickname;
			UserPhotoLink = userProfile.UserPhotoLink;
			City = userProfile.City;
			ClientId = userProfile.ClientId;
			AboutMe = userProfile.AboutMe;
			Contact = userProfile.Contact;
		}

		[Required(ErrorMessage = "Это поле должно быть заполнено"),
		 StringLength(50, MinimumLength = 2, ErrorMessage = "Длина должна быть больше 2 и небольше 50"),
		 RegularExpression("^[^<>]*$", ErrorMessage = "Не должны содержаться знаки < и >")]
		public string Nickname { get; set; }
		public int ClientId { get; set; }

		public string UserPhotoLink { get; set; }

		[StringLength(50, ErrorMessage = "Длина должна быть небольше 50"),
		 RegularExpression("^[^<>]*$", ErrorMessage = "Не должны содержаться знаки < и >")]
		public string City { get; set; }

		[StringLength(500, ErrorMessage = "Максимальная длина 500 символов"),
		 RegularExpression("^[^<>]*$", ErrorMessage = "Не должны содержаться знаки < и >")]
		public string AboutMe { get; set; }

		public Image UserPhoto { get; set; }

		public Contact Contact { get; set; }
	}
}