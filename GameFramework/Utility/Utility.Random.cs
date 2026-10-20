//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System;

namespace GameFramework
{
    public static partial class Utility
    {
        /// <summary>
        /// Random utility functions.
        /// </summary>
        public static class Random
        {
            private static System.Random s_Random = new System.Random((int)DateTime.UtcNow.Ticks);

            /// <summary>
            /// Sets the random seed.
            /// </summary>
            /// <param name="seed">Random seed.</param>
            public static void SetSeed(int seed)
            {
                s_Random = new System.Random(seed);
            }

            /// <summary>
            /// Returns a non-negative random integer.
            /// </summary>
            /// <returns>A 32-bit signed integer that is greater than or equal to 0 and less than System.Int32.MaxValue.</returns>
            public static int GetRandom()
            {
                return s_Random.Next();
            }

            /// <summary>
            /// Returns a non-negative random integer that is less than the specified maximum.
            /// </summary>
            /// <param name="maxValue">The exclusive upper bound of the random number to be generated. maxValue must be greater than or equal to 0.</param>
            /// <returns>A 32-bit signed integer that is greater than or equal to 0, and less than maxValue.</returns>
            public static int GetRandom(int maxValue)
            {
                return s_Random.Next(maxValue);
            }

            /// <summary>
            /// Returns a random integer that is within a specified range.
            /// </summary>
            /// <param name="minValue">The inclusive lower bound of the random number returned.</param>
            /// <param name="maxValue">The exclusive upper bound of the random number returned. maxValue must be greater than or equal to minValue.</param>
            /// <returns>A 32-bit signed integer greater than or equal to minValue and less than maxValue.</returns>
            public static int GetRandom(int minValue, int maxValue)
            {
                return s_Random.Next(minValue, maxValue);
            }

            /// <summary>
            /// Returns a random floating-point number that is greater than or equal to 0.0, and less than 1.0.
            /// </summary>
            /// <returns>A double-precision floating point number that is greater than or equal to 0.0, and less than 1.0.</returns>
            public static double GetRandomDouble()
            {
                return s_Random.NextDouble();
            }

            /// <summary>
            /// Fills the elements of a specified array of bytes with random numbers.
            /// </summary>
            /// <param name="buffer">An array of bytes to contain random numbers.</param>
            public static void GetRandomBytes(byte[] buffer)
            {
                s_Random.NextBytes(buffer);
            }
        }
    }
}
