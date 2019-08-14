namespace Testing
{
    public interface IRandomNumberGenerator
    {
        string Name { get; }
        bool IsSupportsSeed { get; }
        object Generator(int seed);
        uint Next(object generator);
    }
    
    public abstract class RandomNumberGenerator<T> : IRandomNumberGenerator
    {
        public abstract string Name { get; }
        public abstract bool IsSupportsSeed { get; }
        public object Generator(int seed)
        {
            return Create(seed);
        }

        protected abstract T Create(int seed);

        public uint Next(object generator)
        {
            return Next((T) generator);
        }

        protected abstract uint Next(T generator);
    }
}