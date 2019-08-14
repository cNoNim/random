using Testing.Wrappers;
using UnityEngine;

namespace Testing.Sequences
{
    [CreateAssetMenu(menuName = "Random Sequence/PcgHash")]
    public class PcgHashSequence : HashSeedSequence<PcgHashWrapper>
    {
    }
}