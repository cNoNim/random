using Testing.Wrappers;
using UnityEngine;

namespace Testing.Sequences
{
    [CreateAssetMenu(menuName = "Random Sequence/WangHash (vary seed)")]
    public class WangHashVarySeedSequence : HashVarySeedSequence<WangHashWrapper>
    {
    }
}