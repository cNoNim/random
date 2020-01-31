using Hash;

namespace Testing.Wrappers
{
	public class XxHashWrapper : IHashGenerator<uint>
	{
		public string Name => "xxHash";
		public bool IsSupportsSeed => true;

		public uint Create(int seed)
		{
			return (uint) seed;
		}

		public uint Hash(uint generator, int index)
		{
			return XxHash.GetHash(index, generator);
		}
	}

	public class XxHashArrayWrapper : IHashGenerator<uint>
	{
		public string Name => "xxHash (array input)";

		public bool IsSupportsSeed => true;

		public uint Create(int seed)
		{
			return (uint) seed;
		}

		public uint Hash(uint generator, int index)
		{
			return XxHash.GetHash(new[] {index}, generator);
		}
	}
}
