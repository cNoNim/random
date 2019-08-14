namespace Testing.Wrappers
{
    public class RandomWrapper : IRandomNumberGenerator
    {
        public string Name => nameof(System.Random);

        public bool IsSupportsSeed => true;

        public object Generator(int seed)
        {
            return new System.Random(seed);
        }

        public uint Next(object generator)
        {
            return (uint) ((System.Random) generator).Next();
        }
    }
}