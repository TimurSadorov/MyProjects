using System.ComponentModel.DataAnnotations;

namespace BeatTim
{
	public class AuthorizationDto
	{
		protected const string messageForRequired = "Это поле должно быть заполнено";

		[RegularExpression(@"[A-Za-z\-!@%*?<>.\d]+",
			 ErrorMessage = "Логин может содержать только буквы латинского алфавита, цифры и знаки: - ! @ % * ? < > ."),
		 StringLength(100, MinimumLength = 8, ErrorMessage = "Длина должна быть не больше 100 и не меньше 8"),
		 Required(ErrorMessage = messageForRequired)]
		public string Login { get; set; }

		[RegularExpression(@"[A-Za-z\-!@%*?<>\d]+",
			 ErrorMessage = "Пароль может содержать только буквы латинского алфавита, цифры и знаки: - ! @ % * ? < >"),
		 StringLength(50, MinimumLength = 8, ErrorMessage = "Длина должна быть не больше 50 и не меньше 8"),
		 Required(ErrorMessage = messageForRequired)]
		public string Password { get; set; }
		
		[Required(ErrorMessage = messageForRequired),
		 RegularExpression(@"true|false", ErrorMessage = "Должен иметь значение true или false")]
		public string RememberMe { get; set; }
	}
}