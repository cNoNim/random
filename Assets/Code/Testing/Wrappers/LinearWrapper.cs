namespace Testing.Wrappers
{
    public class LinearWrapper : IHashGenerator
    {
        public string Name => "Linear";
        public bool IsSupportsSeed => false;
        public object Generator(int seed)
        {
            return null;
        }

        public uint Hash(object generator, int index)
        {
            return (uint) index * 19969 / 207;
        }
    }
}