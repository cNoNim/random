using Hash;

namespace Testing.Wrappers
{
	public class WangHashWrapper : IHashGenerator<uint>
	{
		public string Name => "WangHash";
		public bool IsSupportsSeed => true;

		public uint Create(int seed)
		{
			return (uint) seed;
		}

		public uint Hash(uint generator, int value)
		{
			return WangHash.GetHash(value, generator);
		}
	}

	public class WangDoubleHashWrapper : IHashGenerator<uint>
	{
		public string Name => "WangDoubleHash";
		public bool IsSupportsSeed => true;

		public uint Create(int seed)
		{
			return (uint) seed;
		}

		public uint Hash(uint generator, int value)
		{
			return WangDoubleHash.GetHash(value, generator);
		}
	}
}
