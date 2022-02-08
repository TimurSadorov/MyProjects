using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeatTim.Models
{
    public class City
    {
        public int CityId { get; set; }
        [StringLength(200), Required]
        public string CityName { get; set; }
    }
}