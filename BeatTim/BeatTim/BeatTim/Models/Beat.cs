using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeatTim.Models
{
    public class Beat
    {
        public int BeatId { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        [MaxLength(100)]
        public string Title { get; set; }
        public string CoverLink { get; set; }
        [Required]
        public string BeatLink { get; set; }
        public int? GenreId { get; set; }
        public Genre Genre { get; set; }
        [Required, Range(0, Int32.MaxValue)]
        public int Price { get; set; }
        public DateTime PublicationDate { get; set; }
        public VerificationStatus VerificationStatus  { get; set; }
        public int NumberAuditions { get; set; }
        [Column(TypeName = "decimal(4, 2)")]
        public double Rating { get; set; }
    }
}