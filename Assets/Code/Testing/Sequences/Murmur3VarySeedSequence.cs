using Testing.Wrappers;
using UnityEngine;

namespace Testing.Sequences
{
    [CreateAssetMenu(menuName = "Random Sequence/Murmur3 (vary seed)")]
    public class Murmur3VarySeedSequence : HashVarySeedSequence<Murmur3Wrapper>
    {
    }
}