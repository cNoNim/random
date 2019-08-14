namespace Testing
{
    public interface IHashGenerator
    {
        string Name { get; }
        bool IsSupportsSeed { get; }
        object Generator(int seed);
        uint Hash(object generator, int value);
    }
    
    public abstract class HashGenerator<T> : IHashGenerator
    {
        public abstract string Name { get; }
        public abstract bool IsSupportsSeed { get; }
        public object Generator(int seed)
        {
            return Create(seed);
        }

        protected abstract T Create(int seed);

        public uint Hash(object generator, int value)
        {
            return Hash((T) generator, value);
        }

        protected abstract uint Hash(T generator, int value);
    }
}