using Testing.Wrappers;
using UnityEngine;

namespace Testing.Sequences
{
	[CreateAssetMenu(menuName = "Random Sequence/xxHash (array input)")]
	public class XxHashArraySequence : HashSeedSequence<XxHashArrayWrapper, uint>
	{
	}
}
