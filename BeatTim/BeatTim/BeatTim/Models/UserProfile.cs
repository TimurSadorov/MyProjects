using System.ComponentModel.DataAnnotations;

namespace BeatTim.Models
{
    public class UserProfile
    {
        public int UserProfileId { get; set; }
        [StringLength(100, MinimumLength = 2), Required]
        public string Nickname { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public string UserPhotoLink { get; set; }
        [StringLength(200)]
        public string City { get; set; }
        public string AboutMe { get; set; }
        public int? ContactId { get; set; }
        public Contact Contact { get; set; }
    }
}