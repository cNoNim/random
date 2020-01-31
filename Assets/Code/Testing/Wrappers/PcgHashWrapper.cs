using Hash;

namespace Testing.Wrappers
{
	public class PcgHashWrapper : IHashGenerator<object>
	{
		public string Name => "PcgHash";
		public bool IsSupportsSeed => false;

		public object Create(int seed)
		{
			return null;
		}

		public uint Hash(object generator, int index)
		{
			return PcgHash.GetHash(index);
		}
	}
}
