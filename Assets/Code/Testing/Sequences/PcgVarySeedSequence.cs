using PRNG;
using Testing.Wrappers;
using UnityEngine;

namespace Testing.Sequences
{
	[CreateAssetMenu(menuName = "Random Sequence/PCG (vary seed)")]
	public class PcgVarySeedSequence : RandomNumberGeneratorVarySeedSequence<PcgWrapper, Pcg>
	{
	}
}
