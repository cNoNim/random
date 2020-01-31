/*
 * This source code file is in the public domain.
 * Permission to use, copy, modify, and distribute this software and its documentation
 * for any purpose and without fee is hereby granted, without any conditions or restrictions.
 * This software is provided “as is” without express or implied warranty.
 *
 * Original code by Rune Skovbo Johansen based on code snippet by Adam Smith.
 */

namespace Hash
{
	public static class PcgHash
	{
		public static uint GetHash(int i)
		{
			var a = i;
			var b = 0;
			for (var r = 0; r < 3; r++)
			{
				a = Rot32((int) ((a ^ 0xcafebabe) + (b ^ 0xfaceb00c)), 23);
				b = Rot32((int) ((a ^ 0xdeadbeef) + (b ^ 0x8badf00d)), 5);
			}

			return (uint) (a ^ b);
		}

		private static int Rot32(int x, int b)
		{
			return (x << b) ^ (x >> (32 - b)); // broken rotate because I'm in java and lazy
		}
	}
}
