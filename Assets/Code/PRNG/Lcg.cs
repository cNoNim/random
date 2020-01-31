namespace PRNG
{
	public class Lcg
	{
		private uint state;

		public Lcg(uint seed = 1)
		{
			state = seed;
		}

		public uint Next()
		{
			const long randMax = 4294967296;
			state = 214013 * state + 2531011;
			return (uint) ((state ^ (state >> 15)) % randMax);
		}
	}
}
