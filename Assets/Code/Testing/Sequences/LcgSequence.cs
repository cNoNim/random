using PRNG;
using Testing.Wrappers;
using UnityEngine;

namespace Testing.Sequences
{
	[CreateAssetMenu(menuName = "Random Sequence/LCG")]
	public class LcgSequence : RandomNumberGeneratorSeedSequence<LcgWrapper, Lcg>
	{
	}
}
