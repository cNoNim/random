using PRNG;

namespace Testing.Wrappers
{
	public class RanduWrapper : IRandomNumberGenerator<Randu>
	{
		public string Name => "Randu";
		public bool IsSupportsSeed => true;

		public Randu Create(int seed)
		{
			return new Randu((uint) seed);
		}

		public uint Next(Randu generator)
		{
			return generator.Next();
		}
	}
}
