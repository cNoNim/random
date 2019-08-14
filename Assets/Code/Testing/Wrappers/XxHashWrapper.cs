using Hash;

namespace Testing.Wrappers
{
    public class XxHashWrapper : HashGenerator<XxHash>
    {
        public override string Name => "xxHash";
        public override bool IsSupportsSeed => true;

        protected override XxHash Create(int seed)
        {
            return new XxHash(seed);
        }

        protected override uint Hash(XxHash generator, int index)
        {
            return generator.GetHash(index);
        }
    }
    
    public class XxHashArrayWrapper : XxHashWrapper
    {
        public override string Name => "xxHash (array input)";

        protected override uint Hash(XxHash generator, int index)
        {
            return generator.GetHash(new[] {index});
        }
    }
}