using PRNG;
using Testing.Wrappers;
using UnityEngine;

namespace Testing.Sequences
{
	[CreateAssetMenu(menuName = "Random Sequence/Unity Random (vary seed)")]
	public class UnityRandomVarySeedSequence : RandomNumberGeneratorVarySeedSequence<UnityRandomWrapper, UnityRandom>
	{
	}
}
