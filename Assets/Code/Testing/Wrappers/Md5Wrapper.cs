using System;
using System.Security.Cryptography;

namespace Testing.Wrappers
{
	public class Md5Wrapper : IHashGenerator<MD5>
	{
		public string Name => "MD5";
		public bool IsSupportsSeed => false;

		public MD5 Create(int seed)
		{
			return MD5.Create();
		}

		public uint Hash(MD5 generator, int index)
		{
			return BitConverter.ToUInt32(generator.ComputeHash(BitConverter.GetBytes(index)), 0);
		}
	}
}
