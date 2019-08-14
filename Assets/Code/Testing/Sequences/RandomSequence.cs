using Testing.Wrappers;
using UnityEngine;

namespace Testing.Sequences
{
    [CreateAssetMenu(menuName = "Random Sequence/Random")]
    public class RandomSequence : RandomNumberGeneratorSeedSequence<RandomWrapper>
    {
    }
}