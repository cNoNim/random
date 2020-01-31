using Hash;

namespace Testing.Wrappers
{
	public class Murmur3Wrapper : IHashGenerator<uint>
	{
		public string Name => "MurMur3";
		public bool IsSupportsSeed => true;

		public uint Create(int seed)
		{
			return (uint) seed;
		}

		public uint Hash(uint generator, int index)
		{
			return MurmurHash.GetHash(index, generator);
		}
	}

	public class Murmur3ArrayWrapper : IHashGenerator<uint>
	{
		public string Name => "MurMur3 (array input)";
		public bool IsSupportsSeed => true;

		public uint Create(int seed)
		{
			return (uint) seed;
		}

		public uint Hash(uint generator, int index)
		{
			return MurmurHash.GetHash(new[] {index}, generator);
		}
	}
}
