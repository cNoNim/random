using Testing.Wrappers;
using UnityEngine;
using Random = System.Random;

namespace Testing.Sequences
{
	[CreateAssetMenu(menuName = "Random Sequence/Random Scrambled (vary seed)")]
	public class RandomScrambledVarySeedSequence : RandomNumberGeneratorVarySeedSequence<RandomScrambledWrapper, Random>
	{
	}
}
