namespace PRNG
{
	public class Randu
	{
		private uint state;

		public Randu(uint seed = 1)
		{
			state = seed;
		}

		public uint Next()
		{
			const long randMax = 2147483648;
			state = 65539 * state + 0;
			return (uint) (state % randMax);
		}
	}
}
