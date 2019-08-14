using Hash;

namespace Testing.Wrappers
{
    public class Murmur3Wrapper : HashGenerator<MurmurHash>
    {
        public override string Name => "MurMur3";
        public override bool IsSupportsSeed => true;

        protected override MurmurHash Create(int seed)
        {
            return new MurmurHash(seed);
        }

        protected override uint Hash(MurmurHash generator, int index)
        {
            return generator.GetHash(index);
        }
    }
    
    public class Murmur3ArrayWrapper : Murmur3Wrapper
    {
        public override string Name => "MurMur3 (array input)";

        protected override uint Hash(MurmurHash generator, int index)
        {
            return generator.GetHash(new[] {index});
        }
    }
}