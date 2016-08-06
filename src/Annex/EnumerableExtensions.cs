#region Copyright (c) 2014-2016 James Snape
// <copyright file="EnumerableExtensions.cs" company="James Snape">
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
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Enumerable Extensions
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Distinct By a specific field.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="keySelector">The key selector.</param>
        /// <returns>The distinct set of items.</returns>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();

            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

        /// <summary>
        /// Iterates over the collection calling the action for each element.
        /// </summary>
        /// <typeparam name="T">The type of item in the sequence.</typeparam>
        /// <param name="sequence">Sequence to iterate</param>
        /// <param name="action">Action to perform</param>
        public static void Do<T>(this IEnumerable<T> sequence, Action<T> action)
        {
            if (sequence == null)
            {
                throw new ArgumentNullException("sequence");
            }

            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            foreach (var item in sequence)
            {
                action(item);
            }
        }

        /// <summary>
        /// Iterates over the collection calling the action for each element.
        /// </summary>
        /// <typeparam name="T">The type of item in the sequence.</typeparam>
        /// <param name="sequence">Sequence to iterate</param>
        /// <param name="action">Action to perform</param>
        public static void Do<T>(this IEnumerable sequence, Action<T> action)
        {
            if (sequence == null)
            {
                throw new ArgumentNullException("sequence");
            }

            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            foreach (T item in sequence)
            {
                action(item);
            }
        }

        /// <summary>
        /// Calls the supplied action for every item in the sequence.
        /// </summary>
        /// <typeparam name="T">Type of item in the sequence.</typeparam>
        /// <param name="sequence">The sequence.</param>
        /// <param name="action">The action.</param>
        /// <returns>A continuation task representing the completion of this asynchronous method call.</returns>
        /// <exception cref="System.ArgumentNullException">If sequence or action is null</exception>
        public static async Task DoAsync<T>(this IEnumerable<T> sequence, Func<T, Task> action)
        {
            if (sequence == null)
            {
                throw new ArgumentNullException("sequence");
            }

            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            foreach (T item in sequence)
            {
                await action(item);
            }
        }

        /// <summary>
        /// Calls the supplied action for every item in the sequence.
        /// </summary>
        /// <typeparam name="T">Type of item in the sequence.</typeparam>
        /// <param name="sequence">The sequence.</param>
        /// <param name="action">The action.</param>
        /// <returns>A continuation task representing the completion of this asynchronous method call.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// If sequence or action is null
        /// </exception>
        public static async Task ParallelDoAsync<T>(this IEnumerable<T> sequence, Func<T, Task> action)
        {
            if (sequence == null)
            {
                throw new ArgumentNullException("sequence");
            }

            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            var tasks = new List<Task>();

            foreach (T item in sequence)
            {
                tasks.Add(action(item));
            }

            await Task.WhenAll(tasks);
        }

        /// <summary>
        /// Uses the specified sequence.
        /// </summary>
        /// <typeparam name="T">The type of item in the sequence.</typeparam>
        /// <typeparam name="TResult">The type of the disposable.</typeparam>
        /// <param name="sequence">The sequence.</param>
        /// <param name="selector">The selector.</param>
        /// <returns>
        /// The same sequence automatically disposed.
        /// </returns>
        public static IEnumerable<T> Use<T, TResult>(this IEnumerable<T> sequence, Func<T, TResult> selector)
        {
            foreach (var item in sequence)
            {
                var disposable = selector(item) as IDisposable;

                try
                {
                    yield return item;
                }
                finally
                {
                    if (disposable != null)
                    {
                        disposable.Dispose();
                    }
                }
            }
        }

        /// <summary>
        /// Uses the specified object.
        /// </summary>
        /// <typeparam name="T">The type of item in the sequence.</typeparam>
        /// <typeparam name="TResult">The type of the disposable.</typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="selector">The selector.</param>
        /// <returns>
        /// A single sequence object.
        /// </returns>
        public static IEnumerable<T> UseSingle<T, TResult>(this T obj, Func<T, TResult> selector) where TResult : IDisposable
        {
            var disposable = selector(obj) as IDisposable;

            try
            {
                yield return obj;
            }
            finally
            {
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }
        }

        /// <summary>
        /// Uses the specified object.
        /// </summary>
        /// <typeparam name="T">The type of item in the sequence.</typeparam>
        /// <param name="obj">The object.</param>
        /// <returns>A single sequence object.</returns>
        public static IEnumerable<T> UseSingle<T>(this T obj) where T : IDisposable
        {
            return UseSingle(obj, o => o);
        }

        /// <summary>
        /// Generates a single value sequence.
        /// </summary>
        /// <typeparam name="T">Type of sequence to generate.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns>A sequence with a single item in it.</returns>
        public static IEnumerable<T> ToEnumerable<T>(this T value)
        {
            yield return value;
        }

        /// <summary>
        /// Generates a sequence from the supplied parameters.
        /// </summary>
        /// <typeparam name="T">Type of sequence to generate.</typeparam>
        /// <param name="values">The values.</param>
        /// <returns>A sequence.</returns>
        public static IEnumerable<T> ToEnumerable<T>(params T[] values)
        {
            return values;
        }
    }
}
