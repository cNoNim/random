using PRNG;

namespace Testing.Wrappers
{
    public class XorShiftWrapper : IRandomNumberGenerator
    {
        public string Name => "XorShift";

        public bool IsSupportsSeed => true;

        public object Generator(int seed)
        {
            return new XorShift(seed);
        }

        public uint Next(object generator)
        {
            return ((XorShift) generator).Next();
        }
    }
}