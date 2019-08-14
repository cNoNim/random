using PRNG;

namespace Testing.Wrappers
{
    public class UnityRandomWrapper : RandomNumberGenerator<UnityRandom>
    {
        public override string Name => "Unity Random";
        public override bool IsSupportsSeed => true;
        protected override UnityRandom Create(int seed)
        {
            return new UnityRandom(seed);
        }

        protected override uint Next(UnityRandom generator)
        {
            return generator.Next();
        }
    }
}