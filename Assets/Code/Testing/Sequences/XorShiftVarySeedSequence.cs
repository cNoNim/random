using PRNG;
using Testing.Wrappers;
using UnityEngine;

namespace Testing.Sequences
{
	[CreateAssetMenu(menuName = "Random Sequence/XorShift (vary seed)")]
	public class XorShiftVarySeedSequence : RandomNumberGeneratorVarySeedSequence<XorShiftWrapper, XorShift>
	{
	}
}
