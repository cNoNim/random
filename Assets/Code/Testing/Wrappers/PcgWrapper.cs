using PRNG;

namespace Testing.Wrappers
{
	public class PcgWrapper : IRandomNumberGenerator<Pcg>
	{
		public string Name => "PCG";
		public bool IsSupportsSeed => true;

		public Pcg Create(int seed)
		{
			return new Pcg((uint) seed);
		}

		public uint Next(Pcg generator)
		{
			return generator.Next();
		}
	}
}
