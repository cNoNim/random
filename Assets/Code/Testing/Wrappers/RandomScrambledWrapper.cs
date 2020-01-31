using System;
using Hash;

namespace Testing.Wrappers
{
	public class RandomScrambledWrapper : IRandomNumberGenerator<Random>
	{
		public string Name => "Random Scrambled";

		public bool IsSupportsSeed => true;

		public Random Create(int seed)
		{
			return new Random((int) XxHash.GetHash(seed, 0u));
		}

		public uint Next(Random generator)
		{
			return (uint) generator.Next();
		}
	}
}
