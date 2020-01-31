using Testing.Wrappers;
using UnityEngine;

namespace Testing.Sequences
{
	[CreateAssetMenu(menuName = "Random Sequence/Murmur3 (array input)")]
	public class Murmur3ArraySequence : HashSeedSequence<Murmur3ArrayWrapper, uint>
	{
	}
}
