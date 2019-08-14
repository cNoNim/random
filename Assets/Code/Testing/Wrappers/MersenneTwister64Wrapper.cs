using PRNG;

namespace Testing.Wrappers
{
    public class MersenneTwister64Wrapper : RandomNumberGenerator<MersenneTwister64>
    {
        public override string Name => "Mersenne Twister 64";
        public override bool IsSupportsSeed => true;
        protected override MersenneTwister64 Create(int seed)
        {
            return new MersenneTwister64((ulong) seed);
        }

        protected override uint Next(MersenneTwister64 generator)
        {
            return (uint) generator.Next();
        }
    }
}