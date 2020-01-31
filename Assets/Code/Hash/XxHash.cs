/*
C# implementation of xxHash optimized for producing random numbers from one or more input integers.
Copyright (C) 2015, Rune Skovbo Johansen. (https://bitbucket.org/runevision/random-numbers-testing/)

Based on C# implementation Copyright (C) 2014, Seok-Ju, Yun. (https://github.com/noricube/xxHashSharp)

Original C Implementation Copyright (C) 2012-2014, Yann Collet. (https://code.google.com/p/xxhash/)
BSD 2-Clause License (http://www.opensource.org/licenses/bsd-license.php)

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are
met:

    * Redistributions of source code must retain the above copyright
      notice, this list of conditions and the following disclaimer.
    * Redistributions in binary form must reproduce the above
      copyright notice, this list of conditions and the following
      disclaimer in the documentation and/or other materials provided
      with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
"AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT
OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/

using System;

namespace Hash
{
	public class XxHash
	{
		private const uint P1 = 2654435761U;
		private const uint P2 = 2246822519U;
		private const uint P3 = 3266489917U;
		private const uint P4 = 668265263U;
		private const uint P5 = 374761393U;

		public static uint GetHash(byte[] buf, uint seed)
		{
			uint h32;
			var index = 0;
			var len = buf.Length;

			if (len >= 16)
			{
				var limit = len - 16;
				var v1 = seed + P1 + P2;
				var v2 = seed + P2;
				var v3 = seed + 0;
				var v4 = seed - P1;

				do
				{
					v1 = SubHash(v1, buf, index);
					index += 4;
					v2 = SubHash(v2, buf, index);
					index += 4;
					v3 = SubHash(v3, buf, index);
					index += 4;
					v4 = SubHash(v4, buf, index);
					index += 4;
				} while (index <= limit);

				h32 = Rot32(v1, 1) + Rot32(v2, 7) + Rot32(v3, 12) + Rot32(v4, 18);
			}
			else
			{
				h32 = seed + P5;
			}

			h32 += (uint) len;

			while (index <= len - 4)
			{
				h32 += BitConverter.ToUInt32(buf, index) * P3;
				h32 = Rot32(h32, 17) * P4;
				index += 4;
			}

			while (index < len)
			{
				h32 += buf[index] * P5;
				h32 = Rot32(h32, 11) * P1;
				index++;
			}

			h32 ^= h32 >> 15;
			h32 *= P2;
			h32 ^= h32 >> 13;
			h32 *= P3;
			h32 ^= h32 >> 16;

			return h32;
		}

		public static uint GetHash(int[] buf, uint seed)
		{
			uint h32;
			var index = 0;
			var len = buf.Length;

			if (len >= 4)
			{
				var limit = len - 4;
				var v1 = seed + P1 + P2;
				var v2 = seed + P2;
				var v3 = seed + 0;
				var v4 = seed - P1;

				do
				{
					v1 = SubHash(v1, (uint) buf[index]);
					index++;
					v2 = SubHash(v2, (uint) buf[index]);
					index++;
					v3 = SubHash(v3, (uint) buf[index]);
					index++;
					v4 = SubHash(v4, (uint) buf[index]);
					index++;
				} while (index <= limit);

				h32 = Rot32(v1, 1) + Rot32(v2, 7) + Rot32(v3, 12) + Rot32(v4, 18);
			}
			else
			{
				h32 = seed + P5;
			}

			h32 += (uint) len * 4;

			while (index < len)
			{
				h32 += (uint) buf[index] * P3;
				h32 = Rot32(h32, 17) * P4;
				index++;
			}

			h32 ^= h32 >> 15;
			h32 *= P2;
			h32 ^= h32 >> 13;
			h32 *= P3;
			h32 ^= h32 >> 16;

			return h32;
		}

		public static uint GetHash(int buf, uint seed)
		{
			var h32 = seed + P5;
			h32 += 4U;
			h32 += (uint) buf * P3;
			h32 = Rot32(h32, 17) * P4;
			h32 ^= h32 >> 15;
			h32 *= P2;
			h32 ^= h32 >> 13;
			h32 *= P3;
			h32 ^= h32 >> 16;
			return h32;
		}

		private static uint SubHash(uint value, byte[] buf, int index)
		{
			return SubHash(value, BitConverter.ToUInt32(buf, index));
		}

		private static uint SubHash(uint value, uint readValue)
		{
			value += readValue * P2;
			value = Rot32(value, 13);
			value *= P1;
			return value;
		}

		private static uint Rot32(uint value, int count)
		{
			return (value << count) | (value >> (32 - count));
		}
	}
}
