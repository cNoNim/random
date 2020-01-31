namespace Testing
{
	public interface IRandomNumberGenerator<T>
	{
		string Name { get; }
		bool IsSupportsSeed { get; }
		T Create(int seed);
		uint Next(T generator);
	}
}
