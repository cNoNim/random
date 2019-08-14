using Testing.Wrappers;
using UnityEngine;

namespace Testing.Sequences
{
    [CreateAssetMenu(menuName = "Random Sequence/MD5")]
    public class Md5Sequence : HashSeedSequence<Md5Wrapper>
    {
    }
}