namespace PRNG
{
	public class Pcg
	{
		private const ulong Multiplier = 6364136223846793005u;
		private const ulong Increment = 1442695040888963407u;

		private ulong state;

		public Pcg(uint seed = 0)
		{
			state = seed + Increment;
			Next();
		}

		public uint Next()
		{
			var x = state;
			var count = (uint) (x >> 59);

			state = x * Multiplier + Increment;
			x ^= x >> 18;
			return Rotr32((uint) (x >> 27), (int) count);
		}

		private static uint Rotr32(uint x, int r)
		{
			return (x >> r) | (x << (-r & 31));
		}
	}
}
