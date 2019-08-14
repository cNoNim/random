using Testing.Wrappers;
using UnityEngine;

namespace Testing.Sequences
{
    [CreateAssetMenu(menuName = "Random Sequence/Murmur3")]
    public class Murmur3Sequence : HashSeedSequence<Murmur3Wrapper>
    {
    }
}