using Hash;

namespace Testing.Wrappers
{
    public class PcgHashWrapper : IHashGenerator
    {
        public string Name => "PcgHash";
        public bool IsSupportsSeed => false;
        public object Generator(int seed)
        {
            return new PcgHash();
        }

        public uint Hash(object generator, int index)
        {
            return ((PcgHash) generator).GetHash(index);
        }
    }
}