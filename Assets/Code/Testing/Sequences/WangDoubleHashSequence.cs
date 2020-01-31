using Testing.Wrappers;
using UnityEngine;

namespace Testing.Sequences
{
	[CreateAssetMenu(menuName = "Random Sequence/WangDoubleHash")]
	public class WangDoubleHashSequence : HashSeedSequence<WangDoubleHashWrapper, uint>
	{
	}
}
