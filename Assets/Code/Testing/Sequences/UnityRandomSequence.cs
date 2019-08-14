using Testing.Wrappers;
using UnityEngine;

namespace Testing.Sequences
{
    [CreateAssetMenu(menuName = "Random Sequence/Unity Random")]
    public class UnityRandomSequence : RandomNumberGeneratorSeedSequence<UnityRandomWrapper>
    {
    }
}