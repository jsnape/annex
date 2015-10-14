#region Copyright (c) 2015 James Snape
// <copyright file="NumberExtensionsFacts.cs" company="James Snape">
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
    using Xunit;

    /// <summary>
    /// Number Extensions Facts
    /// </summary>
    [CLSCompliant(false)]
    public static class NumberExtensionsFacts
    {
        /// <summary>
        /// 0 to base36 should be "0".
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="output">The output.</param>
        [Theory]
        [InlineData(0, "0")]
        [InlineData(10, "a")]
        [InlineData(35, "z")]
        [InlineData(36, "10")]
        [InlineData(-0, "0")]
        [InlineData(-1, "-1")]
        [InlineData(-10, "-a")]
        [InlineData(-35, "-z")]
        [InlineData(-36, "-10")]
        public static void IntToBase36ValuesShouldBeCorrect(int value, string output)
        {
            Assert.Equal(output, value.ToBase36());
        }

        /// <summary>
        /// To the base36 values should be correct.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="output">The output.</param>
        [Theory]
        [InlineData("{eb11fbd7-f26d-4ff3-a1fc-49adc4e2fa6e}", "1t81uc7-m6ml4d-1c2xokh-usjqv8")]
        [InlineData("{00000000-0000-0000-0000-000000000000}", "0-0-0-0")]
        public static void GuidToBase36ValuesShouldBeCorrect(string value, string output)
        {
            Guid id = Guid.Parse(value);

            Assert.Equal(output, id.ToBase36());
        }
    }
}
