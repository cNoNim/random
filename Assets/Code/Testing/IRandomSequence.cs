namespace Testing
{
	public interface IRandomSequence
	{
		string Name { get; }
		uint Next();
		void Reset();
	}
}
