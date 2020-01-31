using System;

namespace Testing.Wrappers
{
	public class RandomWrapper : IRandomNumberGenerator<Random>
	{
		public string Name => nameof(Random);

		public bool IsSupportsSeed => true;

		public Random Create(int seed)
		{
			return new Random(seed);
		}

		public uint Next(Random generator)
		{
			return (uint) generator.Next();
		}
	}
}
