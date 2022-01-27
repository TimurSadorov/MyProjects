using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace BeatTim.Services
{
	public class AudioService
	{
		private static readonly HashSet<string> imageExtensions = new() {".mp3", ".wav"};

		public bool IsCorrectAudioExtension(string fileName) =>
			imageExtensions.Contains(Path.GetExtension(fileName));
	}
}