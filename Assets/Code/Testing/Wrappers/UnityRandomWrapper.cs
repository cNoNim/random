using PRNG;

namespace Testing.Wrappers
{
	public class UnityRandomWrapper : IRandomNumberGenerator<UnityRandom>
	{
		public string Name => "Unity Random";
		public bool IsSupportsSeed => true;

		public UnityRandom Create(int seed)
		{
			return new UnityRandom(seed);
		}

		public uint Next(UnityRandom generator)
		{
			return generator.Next();
		}
	}
}
