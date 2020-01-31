using Testing.Wrappers;
using UnityEngine;
using Random = System.Random;

namespace Testing.Sequences
{
	[CreateAssetMenu(menuName = "Random Sequence/Random")]
	public class RandomSequence : RandomNumberGeneratorSeedSequence<RandomWrapper, Random>
	{
	}
}
