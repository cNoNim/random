using Testing.Wrappers;
using UnityEngine;

namespace Testing.Sequences
{
    [CreateAssetMenu(menuName = "Random Sequence/Mersenne Twister 64")]
    public class MersenneTwister64Sequence : RandomNumberGeneratorSeedSequence<MersenneTwister64Wrapper>
    {
    }
}