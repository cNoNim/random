using Hash;

namespace Testing.Wrappers
{
    public class WangHashWrapper : HashGenerator<WangHash>
    {
        public override string Name => "WangHash";
        public override bool IsSupportsSeed => true;
        protected override WangHash Create(int seed)
        {
            return new WangHash(seed);
        }

        protected override uint Hash(WangHash generator, int value)
        {
            return generator.GetHash(value);
        }
    }
    
    public class WangDoubleHashWrapper : HashGenerator<WangDoubleHash>
    {
        public override string Name => "WangDoubleHash";
        public override bool IsSupportsSeed => true;
        protected override WangDoubleHash Create(int seed)
        {
            return new WangDoubleHash(seed);
        }

        protected override uint Hash(WangDoubleHash generator, int value)
        {
            return generator.GetHash(value);
        }
    }
}