using Testing.Wrappers;
using UnityEngine;

namespace Testing.Sequences
{
    [CreateAssetMenu(menuName = "Random Sequence/LCG (vary seed)")]
    public class LcgVarySeedSequence : RandomNumberGeneratorVarySeedSequence<LcgWrapper>
    {
    }
}