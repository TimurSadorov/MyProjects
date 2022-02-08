using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace BeatTim
{
	public class Cryptographer
	{
		private readonly HashAlgorithm _hashAlgorithm;
		private readonly Random _random;
		

		public Cryptographer(HashAlgorithm hashAlgorithm, Random random)
		{
			_hashAlgorithm = hashAlgorithm;
			_random = random;
		}

		public HashedPassword GetHashedPasswordWithGeneratedSalt(string password)
		{
			var salt = GenerateSalt(15);
			var hashWithSalt = _hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(password + salt));
			return new HashedPassword(salt, string.Concat(hashWithSalt.Select(x => x.ToString("X2"))));
		}
		
		public string GetHashedPasswordWithSalt(string password, string salt)
		{
			if (salt is null)
				throw new AggregateException($"Salt is null");
			var hashWithSalt = _hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(password + salt));
			return string.Concat(hashWithSalt.Select(x => x.ToString("X2")));
		}

		private string GenerateSalt(int length)
		{
			var result = "";
			for (var i = 0; i < length; i++)
				result += (char)('A' + _random.Next(27));
			return result;
		}
	}

	public class HashedPassword
	{
		public HashedPassword(string salt, string hashWithSalt)
		{
			Salt = salt;
			HashWithSalt = hashWithSalt;
		}

		public string Salt { get; }
		public string HashWithSalt { get; }
	}
}