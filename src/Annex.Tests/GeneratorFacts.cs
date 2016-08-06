#region Copyright (c) 2014-2016 James Snape
// <copyright file="GeneratorFacts.cs" company="James Snape">
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
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using Xunit;

    /// <summary>
    /// Generator Facts
    /// </summary>
    public static class GeneratorFacts
    {
        /// <summary>
        /// Tests for generic <c>Generator.Random()</c>
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Justification = "Standard test pattern [JS]")]
        public static class RandomGenerator
        {
            /// <summary>
            /// Random() should generate a sequence of integers.
            /// </summary>
            [Fact]
            public static void RandomIntegerShouldGenerateASequenceOfIntegers()
            {
                var maxCount = 100;

                var count = Generator
                    .RandomInteger()
                    .Take(maxCount)
                    .Count();

                Assert.Equal(maxCount, count);
            }

            /// <summary>
            /// Random() should generate different integers.
            /// </summary>
            [Fact]
            public static void RandomIntegerShouldGenerateDifferentIntegers()
            {
                var maxCount = 100;

                var count = Generator
                    .RandomInteger()
                    .Take(maxCount)
                    .Distinct()
                    .Count();

                Assert.Equal(maxCount, count);
            }
        }
    }
}
