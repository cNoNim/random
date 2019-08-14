using Testing.Wrappers;
using UnityEngine;

namespace Testing.Sequences
{
    [CreateAssetMenu(menuName = "Random Sequence/Mersenne Twister 64 (vary seed)")]
    public class MersenneTwister64VarySeedSequence : RandomNumberGeneratorVarySeedSequence<MersenneTwister64Wrapper>
    {
    }
}