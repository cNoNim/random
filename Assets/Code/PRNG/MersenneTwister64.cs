using System;

namespace PRNG
{
    public class MersenneTwister64
    {
        /** Size of the bytes pool. */
        private const int NN = 312;

        /** Period second parameter. */
        private const int MM = 156;

        /** X * MATRIX_A for X = {0, 1}. */
        private static readonly ulong[] MAG01 = {0x0, 0xb5026f5aa96619e9L};

        /** Most significant 33 bits. */
        private const ulong UM = 0xffffffff80000000L;

        /** Least significant 31 bits. */
        private const ulong LM = 0x7fffffffL;

        /** Bytes pool. */
        private readonly ulong[] mt = new ulong[NN];

        /** Current index in the bytes pool. */
        private uint mti;

        public MersenneTwister64(ulong seed)
        {
            SetSeedInternal(new[] {seed});
        }

        public MersenneTwister64(ulong[] seed)
        {
            SetSeedInternal(seed);
        }

        private void SetSeedInternal(ulong[] seed)
        {
            if (seed.Length == 0)
            {
                // Accept empty seed.
                seed = new ulong[1];
            }

            InitState(19650218L);
            var i = 1;
            var j = 0;

            for (var k = Math.Max(NN, seed.Length); k != 0; k--)
            {
                var mm1 = mt[i - 1];
                mt[i] = (mt[i] ^ ((mm1 ^ (mm1 >> 62)) * 0x369dea0f31a53f85L)) + seed[j] + (ulong) j; // non linear
                i++;
                j++;
                if (i >= NN)
                {
                    mt[0] = mt[NN - 1];
                    i = 1;
                }

                if (j >= seed.Length)
                {
                    j = 0;
                }
            }

            for (var k = NN - 1; k != 0; k--)
            {
                var mm1 = mt[i - 1];
                mt[i] = (mt[i] ^ ((mm1 ^ (mm1 >> 62)) * 0x27bb2ee687b0b0fdL)) - (ulong) i; // non linear
                i++;
                if (i >= NN)
                {
                    mt[0] = mt[NN - 1];
                    i = 1;
                }
            }

            mt[0] = 0x8000000000000000L;
        }

        private void InitState(ulong seed)
        {
            mt[0] = seed;
            for (mti = 1; mti < NN; mti++)
            {
                var mm1 = mt[mti - 1];
                mt[mti] = 0x5851f42d4c957f2dL * (mm1 ^ (mm1 >> 62)) + mti;
            }
        }

        public ulong Next()
        {
            ulong x;

            if (mti >= NN)
            {
                // generate NN words at one time
                for (int i = 0; i < NN - MM; i++)
                {
                    x = (mt[i] & UM) | (mt[i + 1] & LM);
                    mt[i] = mt[i + MM] ^ (x >> 1) ^ MAG01[(int) (x & 0x1L)];
                }

                for (int i = NN - MM; i < NN - 1; i++)
                {
                    x = (mt[i] & UM) | (mt[i + 1] & LM);
                    mt[i] = mt[i + (MM - NN)] ^ (x >> 1) ^ MAG01[(int) (x & 0x1L)];
                }

                x = (mt[NN - 1] & UM) | (mt[0] & LM);
                mt[NN - 1] = mt[MM - 1] ^ (x >> 1) ^ MAG01[(int) (x & 0x1L)];

                mti = 0;
            }

            x = mt[mti++];

            x ^= (x >> 29) & 0x5555555555555555L;
            x ^= (x << 17) & 0x71d67fffeda60000L;
            x ^= (x << 37) & 0xfff7eee000000000L;
            x ^= x >> 43;

            return x;
        }
    }
}