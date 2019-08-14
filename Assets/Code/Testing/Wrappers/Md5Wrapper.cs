using System;
using System.Security.Cryptography;

namespace Testing.Wrappers
{
    public class Md5Wrapper : HashGenerator<MD5>
    {
        public override string Name => "MD5";
        public override bool IsSupportsSeed => false;

        protected override MD5 Create(int seed)
        {
            return MD5.Create();
        }

        protected override uint Hash(MD5 generator, int index)
        {
            return BitConverter.ToUInt32(generator.ComputeHash(BitConverter.GetBytes(index)), 0);
        }
    }
}