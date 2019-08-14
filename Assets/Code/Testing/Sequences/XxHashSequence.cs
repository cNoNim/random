using Testing.Wrappers;
using UnityEngine;

namespace Testing.Sequences
{
    [CreateAssetMenu(menuName = "Random Sequence/xxHash")]
    public class XxHashSequence : HashSeedSequence<XxHashWrapper>
    {
    }
}