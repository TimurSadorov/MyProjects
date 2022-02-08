using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;

namespace BeatTim.Services
{
	public class ImageService
	{
		private static readonly HashSet<string> imageExtensions = new() {".jpeg", ".jpg", ".pjpeg", ".png"};
		
		public bool TryConvertToImage(IFormFile file, out Image image)
		{
			if (imageExtensions.Contains(Path.GetExtension(file.FileName)))
			{
				image = Image.Load(file.OpenReadStream());
				return true;
			}

			image = null;
			return false;
		}
		
		public bool IsValidImageSize(Image image) => 
			image.Height == image.Width && image.Height >= 300;
	}
}