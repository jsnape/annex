#region Copyright (c) 2014 James Snape
// <copyright file="EnumerableExtensionsFacts.cs" company="James Snape">
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

namespace Annex.Tests
{
    using System;
    using System.Collections;
    using Xunit;

    /// <summary>
    /// EnumerableExtensions Facts
    /// </summary>
    public static class EnumerableExtensionsFacts
    {
        /// <summary>
        /// Null Sequence Should Throw
        /// </summary>
        [Fact]
        public static void NullTypedSequenceShouldThrow()
        {
            Assert.Throws(
                typeof(ArgumentNullException),
                () => EnumerableExtensions.Do<int>(null, x => { }));
        }

        /// <summary>
        /// Null Sequence Should Throw
        /// </summary>
        [Fact]
        public static void NullActionOnTypedSequenceShouldThrow()
        {
            var sequence = new int[] { 1, 2, 4, 8 };

            Assert.Throws(
                typeof(ArgumentNullException),
                () => EnumerableExtensions.Do(sequence, null));
        }

        /// <summary>
        /// Do Should Enumerate All Items In Sequence
        /// </summary>
        [Fact]
        public static void DoShouldEnumerateAllItemsInTypedSequence()
        {
            var sequence = new int[] { 1, 2, 4, 8 };
            var count = 0;

            sequence.Do(x => count += x);

            Assert.Equal(15, count);
        }

        /// <summary>
        /// Null Sequence Should Throw
        /// </summary>
        [Fact]
        public static void NullObjectSequenceShouldThrow()
        {
            Assert.Throws(
                typeof(ArgumentNullException),
                () => EnumerableExtensions.Do<int>((IEnumerable)null, x => { }));
        }

        /// <summary>
        /// Null Sequence Should Throw
        /// </summary>
        [Fact]
        public static void NullActionOnObjectSequenceShouldThrow()
        {
            var sequence = (IEnumerable)new int[] { 1, 2, 4, 8 };

            Assert.Throws(
                typeof(ArgumentNullException),
                () => EnumerableExtensions.Do<int>(sequence, null));
        }

        /// <summary>
        /// Do Should Enumerate All Items In Sequence
        /// </summary>
        [Fact]
        public static void DoShouldEnumerateAllItemsInObjectSequence()
        {
            var sequence = (IEnumerable)new int[] { 1, 2, 4, 8 };
            var count = 0;

            sequence.Do<int>(x => count += x);

            Assert.Equal(15, count);
        }
    }
}
