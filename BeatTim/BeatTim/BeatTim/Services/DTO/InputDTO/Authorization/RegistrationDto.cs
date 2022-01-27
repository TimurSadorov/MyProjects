using System.ComponentModel.DataAnnotations;

namespace BeatTim
{
	public class RegistrationDto : AuthorizationDto
	{
		[Required(ErrorMessage = messageForRequired),
		 StringLength(50, MinimumLength = 2, ErrorMessage = "Длина должна быть больше 2 и небольше 50"),
		 RegularExpression("^[^<>]*$", ErrorMessage = "Не должны содержаться знаки < и >")]
		public string Nickname { get; set; }

		[Compare("Password", ErrorMessage = "Пароль не совпадает")]
		public string RepeatedPassword { get; set; }
	}
}