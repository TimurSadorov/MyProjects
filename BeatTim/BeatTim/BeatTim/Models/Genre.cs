using System.ComponentModel.DataAnnotations;

namespace BeatTim.Models
{
    public class Genre
    {
        public int GenreId { get; set; }
        [StringLength(60)]
        public string GenreName { get; set; }
    }
}