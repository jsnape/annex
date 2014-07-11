#region Copyright (c) 2014 James Snape
// <copyright file="DateTimeExtensionsFacts.cs" company="James Snape">
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
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;
    using Xunit.Extensions;

    /// <summary>
    /// DateTimeExtensions Facts
    /// </summary>
    public static class DateTimeExtensionsFacts
    {
        /// <summary>
        /// Gets the DateTime To DateKey Test Cases
        /// </summary>
        public static IEnumerable<object[]> DateTimeToDateKeyCases
        {
            get
            {
                return new[]
                {
                    new object[] { new DateTime(2014, 01, 01), 20140101 },
                    new object[] { new DateTime(2014, 02, 01), 20140201 },
                    new object[] { new DateTime(2014, 02, 02), 20140202 },
                    new object[] { new DateTime(2014, 02, 02, 01, 01, 01), 20140202 },
                    new object[] { DateTime.MinValue, 00010101 },
                    new object[] { DateTime.MaxValue, 99991231 },
                };
            }
        }

        /// <summary>
        /// Gets the DateTime To TimeKey Test Cases
        /// </summary>
        public static IEnumerable<object[]> DateTimeToTimeKeyCases
        {
            get
            {
                return new[]
                {
                    new object[] { new DateTime(2014, 01, 01), 9000000 },
                    new object[] { new DateTime(2014, 02, 01), 9000000 },
                    new object[] { new DateTime(2014, 02, 02), 9000000 },
                    new object[] { new DateTime(2014, 02, 02, 01, 01, 01), 9010101 },
                    new object[] { new DateTime(2014, 02, 02, 01, 02, 01), 9010201 },
                    new object[] { new DateTime(2014, 02, 02, 01, 01, 02), 9010102 },
                    new object[] { DateTime.MinValue, 9000000 },
                    new object[] { DateTime.MaxValue, 9235959 },
                };
            }
        }

        /// <summary>
        /// Gets the DateTimeOffset To DateKey Test Cases
        /// </summary>
        public static IEnumerable<object[]> DateTimeOffsetToDateKeyCases
        {
            get
            {
                return DateTimeToDateKeyCases
                    .Select(x => new object[] { new DateTimeOffset((DateTime)x[0]), x[1] });
            }
        }

        /// <summary>
        /// Gets the DateTimeOffset To TimeKey Test Cases
        /// </summary>
        public static IEnumerable<object[]> DateTimeOffsetToTimeKeyCases
        {
            get
            {
                return DateTimeToTimeKeyCases
                    .Select(x => new object[] { new DateTimeOffset((DateTime)x[0]), x[1] });
            }
        }

        /// <summary>
        /// DateTime To DateKey Conversion Should Be Correct
        /// </summary>
        /// <param name="input">Input value</param>
        /// <param name="expected">Expected value</param>
        [Theory]
        [PropertyData("DateTimeToDateKeyCases")]
        public static void DateTimeToDateKeyConversionShouldBeCorrect(DateTime input, int expected)
        {
            Assert.Equal(expected, input.ToDateKey());
        }

        /// <summary>
        /// DateTime To DateKey Conversion Should Be Correct
        /// </summary>
        /// <param name="input">Input value</param>
        /// <param name="expected">Expected value</param>
        [Theory]
        [PropertyData("DateTimeToTimeKeyCases")]
        public static void DateTimeToTimeKeyConversionShouldBeCorrect(DateTime input, int expected)
        {
            Assert.Equal(expected, input.ToTimeKey());
        }

        /// <summary>
        /// DateTime To DateKey Conversion Should Be Correct
        /// </summary>
        /// <param name="input">Input value</param>
        /// <param name="expected">Expected value</param>
        [Theory]
        [PropertyData("DateTimeOffsetToDateKeyCases")]
        public static void DateTimeOffsetToDateKeyConversionShouldBeCorrect(DateTimeOffset input, int expected)
        {
            Assert.Equal(expected, input.ToDateKey());
        }

        /// <summary>
        /// DateTimeOffset To TimeKey Conversion Should Be Correct
        /// </summary>
        /// <param name="input">Input value</param>
        /// <param name="expected">Expected value</param>
        [Theory]
        [PropertyData("DateTimeOffsetToTimeKeyCases")]
        public static void DateTimeOffsetToTimeKeyConversionShouldBeCorrect(DateTimeOffset input, int expected)
        {
            Assert.Equal(expected, input.ToTimeKey());
        }
    }
}
