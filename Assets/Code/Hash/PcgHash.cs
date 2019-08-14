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
	public class PcgHash
	{
		public uint GetHash(params int[] data)
		{
			return data.Length >= 2 ? GetHash(data[0], data[1]) : GetHash(data[0]);
		}

		public uint GetHash(int i, int j)
		{
			var a = i;
			var b = j;
			for (var r = 0; r < 3; r++)
			{
				a = Rot((int) ((a ^ 0xcafebabe) + (b ^ 0xfaceb00c)), 23);
				b = Rot((int) ((a ^ 0xdeadbeef) + (b ^ 0x8badf00d)), 5);
			}

			return (uint) (a ^ b);
		}

		public uint GetHash(int i)
		{
			var a = i;
			var b = 0;
			for (var r = 0; r < 3; r++)
			{
				a = Rot((int) ((a ^ 0xcafebabe) + (b ^ 0xfaceb00c)), 23);
				b = Rot((int) ((a ^ 0xdeadbeef) + (b ^ 0x8badf00d)), 5);
			}

			return (uint) (a ^ b);
		}

		private static int Rot(int x, int b)
		{
			return (x << b) ^ (x >> (32 - b)); // broken rotate because I'm in java and lazy
		}
	}
}