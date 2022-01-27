using System;
using BeatTim.Models;

namespace BeatTim.Services.DTO.OutputDTO
{
	public class BeatDto
	{
		public BeatDto(Beat beat)
		{
			Title = beat.Title;
			CoverLink = beat.CoverLink;
			BeatLink = beat.BeatLink;
			NumberAuditions = beat.NumberAuditions;
			Rating = beat.Rating;
			Price = beat.Price;
			PublicationDate = beat.PublicationDate;
			BeatId = beat.BeatId;
		}
		public int BeatId { get; set; }
		public string Title { get; set; }
		public string CoverLink { get; set; }
		public string BeatLink { get; set; }
		public int NumberAuditions { get; set; }
		public DateTime PublicationDate { get; set; }
		public double Rating { get; set; }
		public int Price { get; set; }
	}
}