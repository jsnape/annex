#region Copyright (c) 2014-2016 James Snape
// <copyright file="Generator.cs" company="James Snape">
// Permission is hereby granted, free of charge, to any person obtaining a copy
//  of this software and associated documentation files (the "Software"), to deal
//  in the Software without restriction, including without limitation the rights
//  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//  copies of the Software, and to permit persons to whom the Software is
//  furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
//  all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//  THE SOFTWARE.
// </copyright>
#endregion

namespace Annex
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Generator Functions
    /// </summary>
    /// <remarks>
    /// Generators are functions that return an infinite number of elements in a sequence.
    /// Make sure you use Take(n) as a way of limiting the number of items returned.
    /// </remarks>
    public static class Generator
    {
        /// <summary>
        /// Random number sequence generator.
        /// </summary>
        /// <returns>A sequence of random integers.</returns>
        public static IEnumerable<int> RandomInteger()
        {
            var rand = new Random();
            return Repeat(() => rand.Next());
        }

        /// <summary>
        /// Random number sequence generator.
        /// </summary>
        /// <param name="count">The number of times to repeat the value in the generated sequence.</param>
        /// <returns>A sequence of random integers.</returns>
        public static IEnumerable<int> RandomInteger(int count)
        {
            var rand = new Random();
            return Repeat(() => rand.Next(), count);
        }

        /// <summary>
        /// Random number sequence generator.
        /// </summary>
        /// <returns>A sequence of random doubles.</returns>
        public static IEnumerable<double> RandomDouble()
        {
            var rand = new Random();
            return Repeat(() => rand.NextDouble());
        }

        /// <summary>
        /// Random number sequence generator.
        /// </summary>
        /// <param name="count">The number of times to repeat the value in the generated sequence.</param>
        /// <returns>A sequence of random doubles.</returns>
        public static IEnumerable<double> RandomDouble(int count)
        {
            var rand = new Random();
            return Repeat(() => rand.NextDouble(), count);
        }

        /// <summary>
        /// Repeats the specified function to return a sequence of items.
        /// </summary>
        /// <typeparam name="T">The type of the value to be repeated in the result sequence.</typeparam>
        /// <param name="nextItem">The next item generator function.</param>
        /// <param name="count">The number of times to repeat the value in the generated sequence.</param>
        /// <returns>An sequence that contains a repeated value.</returns>
        public static IEnumerable<T> Repeat<T>(Func<T> nextItem, int count)
        {
            return Repeat(nextItem).Take(count);
        }

        /// <summary>
        /// Repeats the specified function to return an infinite sequence of items.
        /// </summary>
        /// <typeparam name="T">The type of the value to be repeated in the result sequence.</typeparam>
        /// <param name="nextItem">The next item generator function.</param>
        /// <returns>An sequence that contains a repeated value.</returns>
        public static IEnumerable<T> Repeat<T>(Func<T> nextItem)
        {
            while (true)
            {
                yield return nextItem();
            }
        }
    }
}
