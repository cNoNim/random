using Testing.Wrappers;
using UnityEngine;

namespace Testing.Sequences
{
    [CreateAssetMenu(menuName = "Random Sequence/xxHash (vary seed)")]
    public class XxHashVarySeedSequence : HashVarySeedSequence<XxHashWrapper>
    {
    }
}