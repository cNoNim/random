/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 *
 * The Initial Developer of the Original Code is Rune Skovbo Johansen.
 * Portions created by the Initial Developer are Copyright (C) 2015
 * the Initial Developer. All Rights Reserved.
 */

namespace Hash
{
	public class WangHash
	{
		private uint seed;

		public WangHash(int seedValue)
		{
			seed = GetHashOfInt((uint) seedValue);
		}

		public uint GetHash(params int[] data)
		{
			var val = seed;
			for (var i = 0; i < data.Length; i++)
				val = GetHashOfInt(val ^ (uint) data[i]);
			return val;
		}

		public uint GetHash(int data)
		{
			return GetHashOfInt(seed ^ (uint) data);
		}

		private uint GetHashOfInt(uint data)
		{
			var val = data;
			// Based on Thomas Wang’s integer hash functions.
			val = (val ^ 61) ^ (val >> 16);
			val *= 9;
			val ^= (val >> 4);
			val *= 0x27d4eb2d;
			val ^= (val >> 15);
			return val;
		}
	}
}