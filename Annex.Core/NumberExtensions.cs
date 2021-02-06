#region Copyright (c) 2015 James Snape
// <copyright file="NumberExtensions.cs" company="James Snape">
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
    using System.Globalization;

    /// <summary>
    /// Number Extensions
    /// </summary>
    public static class NumberExtensions
    {
        /// <summary>
        /// The alpha codes
        /// </summary>
        private const string AlphaCodes = "0123456789abcdefghijklmnopqrstuvwxyz";

        /// <summary>
        /// Converts the supplied integer value to a string of base 36 alphanumeric characters.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <returns>String of converted alpha numeric characters.</returns>
        public static string ToBase36(this int value)
        {
            string result = string.Empty;

            if (value == 0)
            {
                return "0";
            }

            var sign = value < 0 ? "-" : string.Empty;

            value = Math.Abs(value);

            while (value > 0)
            {
                result = AlphaCodes[value % 36] + result;
                value /= 36;
            }

            return sign + result;
        }

        /// <summary>
        /// Converts the supplied integer value to a string of base 36 alphanumeric characters.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <returns>String of converted alpha numeric characters.</returns>
        [CLSCompliant(false)]
        public static string ToBase36(this uint value)
        {
            string result = string.Empty;

            if (value == 0)
            {
                return "0";
            }

            while (value > 0)
            {
                var index = value % 36;
                result = AlphaCodes[(int)index] + result;
                value /= 36;
            }

            return result;
        }

        /// <summary>
        /// Converts a unique identifier to a base 36 string.
        /// </summary>
        /// <param name="id">The unique identifier.</param>
        /// <returns>A base 35 alpha string</returns>
        public static string ToBase36(this Guid id)
        {
            var bytes = id.ToByteArray();

            var part1 = BitConverter.ToUInt32(bytes, 0);
            var part2 = BitConverter.ToUInt32(bytes, 4);
            var part3 = BitConverter.ToUInt32(bytes, 8);
            var part4 = BitConverter.ToUInt32(bytes, 12);

            return string.Format(
                CultureInfo.CurrentCulture,
                "{0}-{1}-{2}-{3}",
                part1.ToBase36(),
                part2.ToBase36(),
                part3.ToBase36(),
                part4.ToBase36());
        }
    }
}
