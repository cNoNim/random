namespace Testing
{
	public interface IHashGenerator<T>
	{
		string Name { get; }
		bool IsSupportsSeed { get; }
		T Create(int seed);
		uint Hash(T generator, int value);
	}
}
