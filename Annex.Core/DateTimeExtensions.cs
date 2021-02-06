#region Copyright (c) 2014 James Snape
// <copyright file="DateTimeExtensions.cs" company="James Snape">
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

    /// <summary>
    /// DateTime Extensions
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Converts a date value to a date key used in data warehouses.
        /// </summary>
        /// <remarks>The format is the compact ISO representation YYYYMMDD.</remarks>
        /// <param name="value">Date value to convert.</param>
        /// <returns>An integer value.</returns>
        public static int ToDateKey(this DateTime value)
        {
            return (value.Year * 10000) + (value.Month * 100) + value.Day;
        }

        /// <summary>
        /// Converts a date value to a date key used in data warehouses.
        /// </summary>
        /// <remarks>The format is the compact ISO representation YYYYMMDD.</remarks>
        /// <param name="value">Date value to convert.</param>
        /// <returns>An integer value.</returns>
        public static int ToDateKey(this DateTimeOffset value)
        {
            return value.Date.ToDateKey();
        }

        /// <summary>
        /// Converts a time value to a date key used in data warehouses.
        /// </summary>
        /// <remarks>
        /// The format is the compact ISO representation HHMMSS.
        /// Also note that any days value will be ignored.
        /// </remarks>
        /// <param name="value">Time value to convert.</param>
        /// <returns>An integer value.</returns>
        public static int ToTimeKey(this TimeSpan value)
        {
            return 9000000 + (value.Hours * 10000) + (value.Minutes * 100) + value.Seconds;
        }

        /// <summary>
        /// Converts a time value to a date key used in data warehouses.
        /// </summary>
        /// <remarks>The format is the compact ISO representation HHMMSS.</remarks>
        /// <param name="value">Time value to convert.</param>
        /// <returns>An integer value.</returns>
        public static int ToTimeKey(this DateTime value)
        {
            return value.TimeOfDay.ToTimeKey();
        }

        /// <summary>
        /// Converts a time value to a date key used in data warehouses.
        /// </summary>
        /// <remarks>The format is the compact ISO representation HHMMSS.</remarks>
        /// <param name="value">Time value to convert.</param>
        /// <returns>An integer value.</returns>
        public static int ToTimeKey(this DateTimeOffset value)
        {
            return value.TimeOfDay.ToTimeKey();
        }
    }
}
