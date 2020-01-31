using UnityEngine;

namespace PRNG
{
	public class UnityRandom
	{
		private Random.State state;

		public UnityRandom(int seed)
		{
			Random.InitState(seed);
			state = Random.state;
		}

		public uint Next()
		{
			Random.state = state;
			var result = Random.Range(int.MinValue, int.MaxValue);
			state = Random.state;
			return (uint) result;
		}
	}
}
