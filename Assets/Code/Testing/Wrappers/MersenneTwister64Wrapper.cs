using PRNG;

namespace Testing.Wrappers
{
	public class MersenneTwister64Wrapper : IRandomNumberGenerator<MersenneTwister64>
	{
		public string Name => "Mersenne Twister 64";
		public bool IsSupportsSeed => true;

		public MersenneTwister64 Create(int seed)
		{
			return new MersenneTwister64((ulong) seed);
		}

		public uint Next(MersenneTwister64 generator)
		{
			return (uint) generator.Next();
		}
	}
}
