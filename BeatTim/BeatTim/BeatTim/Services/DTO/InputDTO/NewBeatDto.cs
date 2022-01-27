using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;

namespace BeatTim.DTO.InputDTO
{
	public class NewBeatDto
	{
		[MaxLength(100, ErrorMessage = "Длина названия не больше 100 символов"),
		 Required(ErrorMessage = "Это поле должно быть заполнено")]
		public string Title { get; set; }

		public Image Cover { get; set; }

		[Required(ErrorMessage = "Это поле должно быть заполнено"),
		 Range(0, Int32.MaxValue, ErrorMessage = "Значение не меньше 0 и не больше 2 млрд.")]
		public int Price { get; set; }

		public string GenreName { get; set; }
		public IFormFile Audio { get; set; }
	}
}