/* 
 * This source code file is in the public domain.
 * Permission to use, copy, modify, and distribute this software and its documentation
 * for any purpose and without fee is hereby granted, without any conditions or restrictions.
 * This software is provided “as is” without express or implied warranty.
 * 
 * Original version of this code can be obtained from
 * http://www.fourmilab.ch/random/
 */

using System;

namespace Ent
{
	/// <summary>
	///	Apply various randomness tests to a stream of bytes
	///	Original code by John Walker  --  September 1996
	///	http://www.fourmilab.ch/
	///	
	/// C# port of ENT (ent - pseudorandom number sequence test)
	/// by Brett Trotter
	/// blt@iastate.edu
	/// </summary>

	public class EntCalc
	{
		static readonly double[,] Chsqt =
		{
			{0.5, 0.25, 0.1, 0.05, 0.025, 0.01, 0.005, 0.001, 0.0005, 0.0001},
			{0.0, 0.6745, 1.2816, 1.6449, 1.9600, 2.3263, 2.5758, 3.0902, 3.2905, 3.7190}
		};

		private static readonly int Monten = 6; /* Bytes used as Monte Carlo co-ordinates
													 * This should be no more bits than the mantissa 
													 * of your "double" floating point type. */

		private readonly uint[] monte = new uint[Monten];
		private readonly double[] prob = new double[256]; /* Probabilities per bin for entropy */
		private readonly long[] ccount = new long[256]; /* Bins to count occurrences of values */
		private long totalc; /* Total bytes counted */

		private int mp;
		private bool sccfirst;
		private long inmont, mcount;
		private double a;
		private double cexp;
		private readonly double incirc;
		private double montex, montey, montepi;
		private double scc, sccun, sccu0, scclast, scct1, scct2, scct3;
		private double ent, chisq, datasum;

		private readonly bool binary; /* Treat input as a bitstream */

		public struct EntCalcResult
		{
			public double Entropy;
			public double ChiSquare;
			public double Mean;
			public double MonteCarloPiCalc;
			public double SerialCorrelation;
			public long[] OccuranceCount;

			public double ChiProbability;
			public double MonteCarloErrorPct;
			public double OptimumCompressionReductionPct;
			public double ExpectedMeanForRandom;

			public long NumberOfSamples;
		}


		/*  Initialise random test counters.  */
		public EntCalc(bool binmode)
		{
			int i;

			binary = binmode; /* Set binary / byte mode */

			/* Initialise for calculations */

			ent = 0.0; /* Clear entropy accumulator */
			chisq = 0.0; /* Clear Chi-Square */
			datasum = 0.0; /* Clear sum of bytes for arithmetic mean */

			mp = 0; /* Reset Monte Carlo accumulator pointer */
			mcount = 0; /* Clear Monte Carlo tries */
			inmont = 0; /* Clear Monte Carlo inside count */
			incirc = 65535.0 * 65535.0; /* In-circle distance for Monte Carlo */

			sccfirst = true; /* Mark first time for serial correlation */
			scct1 = scct2 = scct3 = 0.0; /* Clear serial correlation terms */

			incirc = Math.Pow(Math.Pow(256.0, Monten / 2) - 1, 2.0);

			for (i = 0; i < 256; i++) 
				ccount[i] = 0;

			totalc = 0;
		}


		/*  AddSample  --	Add one or more bytes to accumulation.	*/
		public void AddSample(byte[] buf)
		{
			foreach (var bufByte in buf)
			{
				var bean = 0;

				var oc = bufByte;

				do
				{
					int c;
					if (binary)
					{
						c = oc & 0x80; // Get the MSB of the byte being read in
					}
					else
					{
						c = oc;
					}

					ccount[c]++; /* Update counter for this bin */
					totalc++;

					/* Update inside / outside circle counts for Monte Carlo computation of PI */

					if (bean == 0)
					{
						monte[mp++] = oc; /* Save character for Monte Carlo */
						if (mp >= Monten)
						{
							/* Calculate every MONTEN character */
							int mj;

							mp = 0;
							mcount++;
							montex = montey = 0;
							for (mj = 0; mj < Monten / 2; mj++)
							{
								montex = montex * 256.0 + monte[mj];
								montey = montey * 256.0 + monte[Monten / 2 + mj];
							}

							if (montex * montex + montey * montey <= incirc)
							{
								inmont++;
							}
						}
					}

					/* Update calculation of serial correlation coefficient */

					sccun = c;
					if (sccfirst)
					{
						sccfirst = false;
						scclast = 0;
						sccu0 = sccun;
					}
					else
					{
						scct1 = scct1 + scclast * sccun;
					}

					scct2 = scct2 + sccun;
					scct3 = scct3 + sccun * sccun;
					scclast = sccun;
					oc <<= 1; // left shift by one
				} while (binary && ++bean < 8
				); // keep looping if we're in binary mode and while the bean counter is less than 8 (bits)
			}
		} // end foreach


		/*  EndCalculation  --	Complete calculation and return results.  */
		public EntCalcResult EndCalculation()
		{
			int i;

			/* Complete calculation of serial correlation coefficient */

			scct1 = scct1 + scclast * sccu0;
			scct2 = scct2 * scct2;
			scc = totalc * scct3 - scct2;
			if (scc == 0.0)
				scc = -100000;
			else
				scc = (totalc * scct1 - scct2) / scc;

			/* Scan bins and calculate probability for each bin and
			   Chi-Square distribution */

			cexp = totalc / (binary ? 2.0 : 256.0); /* Expected count per bin */
			for (i = 0; i < (binary ? 2 : 256); i++)
			{
				prob[i] = (double) ccount[i] / totalc;
				a = ccount[i] - cexp;
				chisq = chisq + a * a / cexp;
				datasum += (double) i * ccount[i];
			}

			/* Calculate entropy */

			for (i = 0; i < (binary ? 2 : 256); i++)
			{
				if (prob[i] > 0.0)
				{
					ent += prob[i] * Log2(1 / prob[i]);
				}
			}

			/* Calculate Monte Carlo value for PI from percentage of hits
			   within the circle */

			montepi = 4.0 * ((double) inmont / mcount);


			/* Calculate probability of observed distribution occurring from
			   the results of the Chi-Square test */

			var chip = Math.Sqrt(2.0 * chisq) - Math.Sqrt(2.0 * (binary ? 1 : 255.0) - 1.0);
			a = Math.Abs(chip);
			for (i = 9; i >= 0; i--)
			{
				if (Chsqt[1, i] < a)
				{
					break;
				}
			}

			chip = chip >= 0.0 ? Chsqt[0, i] : 1.0 - Chsqt[0, i];

			var compReductionPct = ((binary ? 1 : 8) - ent) / (binary ? 1.0 : 8.0);

			/* Return results */
			var result = new EntCalcResult
			{
				Entropy = ent,
				ChiSquare = chisq,
				ChiProbability = chip,
				Mean = datasum / totalc,
				ExpectedMeanForRandom = binary ? 0.5 : 127.5,
				MonteCarloPiCalc = montepi,
				MonteCarloErrorPct = Math.Abs(Math.PI - montepi) / Math.PI,
				SerialCorrelation = scc,
				OptimumCompressionReductionPct = compReductionPct,
				OccuranceCount = ccount,
				NumberOfSamples = totalc
			};
			return result;
		}


		/*  LOG2  --  Calculate log to the base 2  */
		static double Log2(double x)
		{
			return Math.Log(x, 2); //can use this in C#
		}
	}
}