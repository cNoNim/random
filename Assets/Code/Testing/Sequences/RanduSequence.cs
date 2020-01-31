using PRNG;
using Testing.Wrappers;
using UnityEngine;

namespace Testing.Sequences
{
	[CreateAssetMenu(menuName = "Random Sequence/Randu")]
	public class RanduSequence : RandomNumberGeneratorSeedSequence<RanduWrapper, Randu>
	{
	}
}
