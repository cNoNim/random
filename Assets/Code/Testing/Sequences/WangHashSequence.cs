using Testing.Wrappers;
using UnityEngine;

namespace Testing.Sequences
{
    [CreateAssetMenu(menuName = "Random Sequence/WangHash")]
    public class WangHashSequence : HashSeedSequence<WangHashWrapper>
    {
    }
}