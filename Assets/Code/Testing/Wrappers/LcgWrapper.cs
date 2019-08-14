using PRNG;

namespace Testing.Wrappers
{
    public class LcgWrapper : RandomNumberGenerator<Lcg>
    {
        public override string Name => "LCG";
        public override bool IsSupportsSeed => true;

        protected override Lcg Create(int seed)
        {
            return new Lcg((uint) seed);
        }

        protected override uint Next(Lcg generator)
        {
            return generator.Next();
        }
    }
}