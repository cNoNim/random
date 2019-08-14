using Testing.Wrappers;
using UnityEngine;

namespace Testing.Sequences
{
    [CreateAssetMenu(menuName = "Random Sequence/XorShift")]
    public class XorShiftSequence : RandomNumberGeneratorSeedSequence<XorShiftWrapper>
    {
    }
}