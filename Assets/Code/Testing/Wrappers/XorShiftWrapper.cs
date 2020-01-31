using PRNG;

namespace Testing.Wrappers
{
	public class XorShiftWrapper : IRandomNumberGenerator<XorShift>
	{
		public string Name => "XorShift";

		public bool IsSupportsSeed => true;

		public XorShift Create(int seed)
		{
			return new XorShift(seed);
		}

		public uint Next(XorShift generator)
		{
			return generator.Next();
		}
	}
}
