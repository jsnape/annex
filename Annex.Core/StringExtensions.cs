#region Copyright (c) 2014-2016 James Snape
// <copyright file="StringExtensions.cs" company="James Snape">
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
    using System.Linq;
    using System.Text.RegularExpressions;

    /// <summary>
    /// String Extensions
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Removes all chars that match the predicate.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="condition">The condition.</param>
        /// <returns>
        /// The same string with characters removed.
        /// </returns>
        /// <exception cref="ArgumentNullException">condition is null.</exception>
        public static string RemoveAll(this string input, Predicate<char> condition)
        {
            if (condition == null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            if (input == null)
            {
                return null;
            }

            var chars = input
                .Where(c => !condition(c))
                .ToArray();

            return new string(chars);
        }

        /// <summary>
        /// Removes all whitespace.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The same string with all whitespace removed.</returns>
        public static string RemoveAllWhiteSpace(this string input)
        {
            return input.RemoveAll(c => char.IsWhiteSpace(c));
        }

        /// <summary>
        /// Returns the soundex value for the input string.
        /// </summary>
        /// <remarks>
        /// Based on this algorithm: https://en.wikipedia.org/wiki/Soundex
        /// This looks a better version though: http://www.blackwasp.co.uk/Soundex.aspx
        /// </remarks>
        /// <param name="input">The input.</param>
        /// <returns>
        /// The soundex value for the string.
        /// </returns>
        public static string Soundex(this string input)
        {
            var word = input
                .RemoveAll(c => !char.IsLetter(c))
                .ToUpperInvariant();

            if (string.IsNullOrEmpty(word))
            {
                return null;
            }

            var soundex = Regex.Replace(word.Substring(1), "[AEIOUYHW]", string.Empty);
            soundex = Regex.Replace(soundex, "[BFPV]+", "1");
            soundex = Regex.Replace(soundex, "[CGJKQSXZ]+", "2");
            soundex = Regex.Replace(soundex, "[DT]+", "3");
            soundex = Regex.Replace(soundex, "[L]+", "4");
            soundex = Regex.Replace(soundex, "[MN]+", "5");
            soundex = Regex.Replace(soundex, "[R]+", "6");

            soundex = word[0] + soundex;

            return soundex.PadRight(4, '0').Substring(0, 4);
        }
    }
}
