using Testing.Wrappers;
using UnityEngine;
using Random = System.Random;

namespace Testing.Sequences
{
	[CreateAssetMenu(menuName = "Random Sequence/Random Scrambled")]
	public class RandomScrambledSequence : RandomNumberGeneratorSeedSequence<RandomScrambledWrapper, Random>
	{
	}
}
