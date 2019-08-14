using Testing.Wrappers;
using UnityEngine;

namespace Testing.Sequences
{
    [CreateAssetMenu(menuName = "Random Sequence/Linear")]
    public class LinearSequence : HashSeedSequence<LinearWrapper>
    {
    }
}