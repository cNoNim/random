using PRNG;

namespace Testing.Wrappers
{
	public class MersenneTwister64Wrapper : IRandomNumberGenerator<MersenneTwister64>
	{
		public string Name => "Mersenne Twister 64";
		public bool IsSupportsSeed => true;

		private ulong[] _seed = new ulong[1];

		public MersenneTwister64 Create(int seed)
		{
			_seed[0] = (ulong) seed;
			return new MersenneTwister64(_seed);
		}

		public uint Next(MersenneTwister64 generator)
		{
			return (uint) generator.Next();
		}
	}
}
