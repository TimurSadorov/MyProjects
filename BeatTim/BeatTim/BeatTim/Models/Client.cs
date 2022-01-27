using System.Collections.Generic;

namespace BeatTim.Models
{
	public class Client
	{
		public int ClientId { get; set; }
		public ClientRole ClientRole { get; set; }
		public UserProfile UserProfile { get; set; }
	}
}