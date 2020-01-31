using PRNG;

namespace Testing.Wrappers
{
	public class LcgWrapper : IRandomNumberGenerator<Lcg>
	{
		public string Name => "LCG";
		public bool IsSupportsSeed => true;

		public Lcg Create(int seed)
		{
			return new Lcg((uint) seed);
		}

		public uint Next(Lcg generator)
		{
			return generator.Next();
		}
	}
}
