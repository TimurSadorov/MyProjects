using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeatTim.Models
{
    public class LoginDetails
    {
        public int LoginDetailsId { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        [StringLength(100, MinimumLength = 8), Required]
        public string Login { get; set; }
        [StringLength(300, MinimumLength = 8), Required]
        public string HashedPassword { get; set; }
        [Required]
        public string Salt { get; set; }
    }
}