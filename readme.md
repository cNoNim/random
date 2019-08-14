# Random Numbers Testing

This repository contains C# implementations of xxHash, MurmurHash3, and select other hash functions and random number generators (RNGs). These are modified and optimized to be more suitable and efficient for procedural generation.

The repository also contains C# code for a random numbers testing framework which can be used to compare the randomness quality and performance of said hash functions and RNGs.

Also see original blog post at http://blog.runevision.com/2015/01/primer-on-repeatable-random-numbers.html


## Random numbers testing framework

The random numbers testing framework is divided into a front-end part which runs on top of the Unity engine (version 4.6 or later) and a back-end part which does not rely on the Unity editor or API.

The framework can be used to evaluate randomness based on these tests:

* Visual plot of sequence of 65536 random numbers.
* Visual plot of 50000 random coordinates.
* Calculated Serial Correlation (test from ENT framework).
* Calculated Monte Carlo Pi Value (test from ENT framework).
* Calculated Diagonals Deviation (numerical measure of whether the random coordinates tend to be concentrates on certain diagonals.
* Measured execution time (will vary dependent on machine the tests are run on).


## Random hash function implementations

xxHash and MurmurHash3 are random hash functions with very good randomness properties and good performance. The implementations in this repository has been modified to be more suitable for procedural generation:

* Added overloads that takes an int array instead of byte array (less code and executes faster).
* Added methods for obtaining random integers or floats in given ranges.
* Added overloads optimized for single int input with no loop in the implementation.

The implementations are in C# and have no dependencies on Unity.


## Contributions

Random Numbers Testing

* Original framework code by Rune Skovbo Johansen - http://runevision.com
* See files of individual hash function and RNG implementations for respective credits.

ENT (ent - pseudorandom number sequence test)

* Ported to C# by Brett Trotter - blt@iastate.edu - https://github.com/BrettTrotter/
* Original code by John Walker - http://www.fourmilab.ch/


## License

Most files in the repository are licensed under the Mozilla Public License, v. 2.0. A few files are in the public domain and thus have no restrictions on their use. See the individual files for details.

A copy of the MPL can be obtained at [http://mozilla.org/MPL/2.0/](http://mozilla.org/MPL/2.0/).