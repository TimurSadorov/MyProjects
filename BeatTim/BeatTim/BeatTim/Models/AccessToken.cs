using System.ComponentModel.DataAnnotations;

namespace BeatTim.Models
{
	public class AccessToken
	{
		public int AccessTokenId { get; set; }
		public int ClientId { get; set; }
		public Client Client { get; set; }
		[Required, StringLength(200)]
		public string Value { get; set; }
	}
}