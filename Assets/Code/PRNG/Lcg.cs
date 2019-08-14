namespace PRNG
{
    public class Lcg
    {
        private uint next;

        public Lcg(uint seed = 1)
        {
            next = seed;
        }

        public uint Next()
        {
            const int randMax = 32767;
            next = 214013 * next + 2531011;
            return (next ^ (next >> 15)) % (randMax + 1);
        }
    }
}