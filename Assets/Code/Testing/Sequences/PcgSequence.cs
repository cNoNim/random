using PRNG;
using Testing.Wrappers;
using UnityEngine;

namespace Testing.Sequences
{
	[CreateAssetMenu(menuName = "Random Sequence/PCG")]
	public class PcgSequence : RandomNumberGeneratorSeedSequence<PcgWrapper, Pcg>
	{
	}
}
