/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 *
 * The Initial Developer of the Original Code is Rune Skovbo Johansen.
 * Portions created by the Initial Developer are Copyright (C) 2015
 * the Initial Developer. All Rights Reserved.
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Testing
{
	public class RandomnessTest {
		private readonly IRandomSequence sequence;
		public readonly string Name;
		public string Result;
		public readonly float[] NoiseSequence = new float[256*256];
		private readonly int[,] coords = new int[256,256];
		public readonly float[,] CoordsArray = new float[256,256];
		private readonly float[] diagonalSums = new float[256];
		private float diagonalsDeviation;
		private const int ByteIndex = 0;

		public RandomnessTest (IRandomSequence randomSequence) {
			sequence = randomSequence;
			Name = sequence.Name.Replace ("#", "0\u2026n");
			Test ();
			Console.WriteLine ("\n"+Name);
			Console.WriteLine (Result);
		}

		private uint GetBytePart (uint i, int byteIndex) {
			return ((i >> (8 * byteIndex)) % 256 + 256) % 256;
		}

		public void Reset () {
			sequence.Reset ();
		}

		private void Test () {
			var ints = new uint[500000];
		
			// Call random function
			sequence.Reset ();
			var stopWatch = new Stopwatch ();
			stopWatch.Start ();
			for (var i=0; i<ints.Length; i++)
				ints[i] = sequence.Next ();
			stopWatch.Stop ();
		
			// Convert to bytes
			var bytes = new byte[ints.Length];
			for (var i = 0; i < bytes.Length; i++)
				bytes[i] = (byte)GetBytePart (ints[i], ByteIndex);
		
			// Test randomness data
			var ent = new Ent.EntCalc (false);
			ent.AddSample (bytes);
			var calcResult = ent.EndCalculation ();
		
			// Create noise sequence
			for (var i=0; i < NoiseSequence.Length; i++)
				NoiseSequence[i] = GetBytePart (ints[i], ByteIndex) / 255f;
		
			// Create coords data
			var max = 0;
			for (var i=0; i < ints.Length; i += 2) {
				var x = GetBytePart (ints[i], ByteIndex);
				var y = GetBytePart (ints[i + 1], ByteIndex);
				var value = coords[x,y];
				value++;
				max = Mathf.Max(value, max);
				coords[x,y] = value;
			}

			// Calculate coords results
			for (var j=0; j<256; j++) {
				for (var i=0; i<256; i++) {
					var value = coords[i,j] / (float)max;
					CoordsArray[i,j] = value;
					diagonalSums[(i + j) % 256] += value * 0.5f;
					diagonalSums[(i - j + 256) % 256] += value * 0.5f;
				}
			}
			diagonalsDeviation = StandardDeviation (new List<float> (diagonalSums));

			// Get string with result
			Result = GetResult (calcResult, stopWatch.ElapsedMilliseconds);
		}

		private static float StandardDeviation (List<float> valueList) {
			var m = 0.0f;
			var s = 0.0f;
			var k = 1;
			foreach (var value in valueList) 
			{
				var tmpM = m;
				m += (value - tmpM) / k;
				s += (value - tmpM) * (value - m);
				k++;
			}
			return (float)Math.Sqrt (s / (k-1));
		}

		private string GetResult (Ent.EntCalc.EntCalcResult result, float duration) {
			var meanValueQuality = Clamp01 (1 - Math.Abs (127.5 - result.Mean) / 128);
			var serialCorrelationQuality = Clamp01 (1 - 2 * Math.Abs (result.SerialCorrelation));
			var piQuality = Clamp01 (1 - 10 * result.MonteCarloErrorPct);
			var diagonalsDeviationQuality = Clamp01 (1 - diagonalsDeviation / 256);
			var combined = Math.Min (Math.Min (Math.Min (meanValueQuality, serialCorrelationQuality), piQuality), diagonalsDeviationQuality);

			return "                             value quality\n" 
			       +      $"Mean Value:               {result.Mean,8:F4} {meanValueQuality,7:P0}\n" 
			       +      $"Serial Correlation:       {Math.Max(0, result.SerialCorrelation),8:F4} {serialCorrelationQuality,7:P0}\n" 
			       +      $"Monte Carlo Pi Value:     {result.MonteCarloPiCalc,8:F4} {piQuality,7:P0}\n" 
			       +      $"Diagonals Deviation:      {diagonalsDeviation,8:F4} {diagonalsDeviationQuality,7:P0}\n" 
			       +      $"<b>Overall Quality:                   {combined,7:P0}</b>\n\n" 
			       +      $"Execution Time:                  {duration,6} ms";
		}
	
		private static double Clamp01 (double val) {
			return Clamp (val, 0, 1);
		}
	
		private static double Clamp (double val, double min, double max) {
			return Math.Min (max, Math.Max (val, min));
		}
	}
}