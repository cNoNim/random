using Testing.Wrappers;
using UnityEngine;
using Random = System.Random;

namespace Testing.Sequences
{
	[CreateAssetMenu(menuName = "Random Sequence/Random (vary seed)")]
	public class RandomVarySeedSequence : RandomNumberGeneratorVarySeedSequence<RandomWrapper, Random>
	{
	}
}
