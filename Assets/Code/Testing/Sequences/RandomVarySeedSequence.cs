using Testing.Wrappers;
using UnityEngine;

namespace Testing.Sequences
{
    [CreateAssetMenu(menuName = "Random Sequence/Random (vary seed)")]
    public class RandomVarySeedSequence : RandomNumberGeneratorVarySeedSequence<RandomWrapper>
    {
    }
}