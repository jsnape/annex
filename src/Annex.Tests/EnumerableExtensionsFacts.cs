#region Copyright (c) 2014-2016 James Snape
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
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Threading.Tasks;
    using NSubstitute;
    using Ploeh.AutoFixture;
    using Xunit;

    /// <summary>
    /// EnumerableExtensions Facts
    /// </summary>
    public static class EnumerableExtensionsFacts
    {
        /// <summary>
        /// Tests for object IEnumerable.Do()
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Justification = "Standard test pattern [JS]")]
        public static class ObjectEnumerable
        {
            /// <summary>
            /// Do() should iterate all items in collection.
            /// </summary>
            [Fact]
            public static void DoShouldIterateAllItemsInCollection()
            {
                var fixture = new Fixture();
                var items = (IEnumerable)fixture.CreateMany<int>();
                var expected = items.Cast<int>().Sum();

                int sum = 0;
                items.Do<int>(x => sum += x);

                Assert.Equal(expected, sum);
            }

            /// <summary>
            /// Do() should throw when null sequence passed.
            /// </summary>
            [Fact]
            public static void DoShouldThrowWhenNullSequencePassed()
            {
                Action action = () => EnumerableExtensions.Do<int>((IEnumerable)null, null);

                var exception = Assert.Throws<ArgumentNullException>(action);
                Assert.Equal("sequence", exception.ParamName);
            }

            /// <summary>
            /// Do() should throw when null action passed.
            /// </summary>
            [Fact]
            public static void DoShouldThrowWhenNullActionPassed()
            {
                var fixture = new Fixture();
                var items = (IEnumerable)fixture.CreateMany<int>();

                Action action = () => EnumerableExtensions.Do<int>(items, null);

                var exception = Assert.Throws<ArgumentNullException>(action);
                Assert.Equal("action", exception.ParamName);
            }
        }

        /// <summary>
        /// Tests for generic <c>IEnumerable{T}.Do()</c>
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Justification = "Standard test pattern [JS]")]
        public static class GenericEnumerable
        {
            /// <summary>
            /// Do() should iterate all items in collection.
            /// </summary>
            [Fact]
            public static void DoShouldIterateAllItemsInCollection()
            {
                var fixture = new Fixture();
                var items = fixture.CreateMany<int>();
                var expected = items.Sum();

                int sum = 0;
                items.Do(x => sum += x);

                Assert.Equal(expected, sum);
            }

            /// <summary>
            /// Do() should throw when null sequence passed.
            /// </summary>
            [Fact]
            public static void DoShouldThrowWhenNullSequencePassed()
            {
                Action action = () => EnumerableExtensions.Do<int>(null, null);

                var exception = Assert.Throws<ArgumentNullException>(action);
                Assert.Equal("sequence", exception.ParamName);
            }

            /// <summary>
            /// Do() should throw when null action passed.
            /// </summary>
            [Fact]
            public static void DoShouldThrowWhenNullActionPassed()
            {
                var fixture = new Fixture();
                var items = fixture.CreateMany<int>();

                Action action = () => EnumerableExtensions.Do<int>(items, null);

                var exception = Assert.Throws<ArgumentNullException>(action);
                Assert.Equal("action", exception.ParamName);
            }

            /// <summary>
            /// DoAsync() should iterate all items in collection.
            /// </summary>
            /// <returns>A continuation task representing the completion of this asynchronous method call.</returns>
            [Fact]
            public static async Task DoAsyncShouldIterateAllItemsInCollection()
            {
                var fixture = new Fixture();
                var items = fixture.CreateMany<int>();
                var expected = items.Sum();

                int sum = 0;
                await items.DoAsync(async (x) =>
                {
                    sum += x;
                    await Task.FromResult(0);
                });

                Assert.Equal(expected, sum);
            }

            /// <summary>
            /// Do() should throw when null sequence passed.
            /// </summary>
            [Fact]
            public static void DoAsyncShouldThrowWhenNullSequencePassed()
            {
                Action action = () => EnumerableExtensions.DoAsync<int>(null, null).Wait();

                var exception = Assert.Throws<AggregateException>(action);
                var inner = exception.InnerExceptions.Single() as ArgumentNullException;

                Assert.NotNull(inner);
                Assert.Equal("sequence", inner.ParamName);
            }

            /// <summary>
            /// DoAsync() should throw when null action passed.
            /// </summary>
            [Fact]
            public static void DoAsyncShouldThrowWhenNullActionPassed()
            {
                var fixture = new Fixture();
                var items = fixture.CreateMany<int>();

                Action action = () => EnumerableExtensions.DoAsync<int>(items, null).Wait();

                var exception = Assert.Throws<AggregateException>(action);
                var inner = exception.InnerExceptions.Single() as ArgumentNullException;

                Assert.NotNull(inner);
                Assert.Equal("action", inner.ParamName);
            }

            /// <summary>
            /// ParallelDoAsync() should iterate all items in collection.
            /// </summary>
            /// <returns>A continuation task representing the completion of this asynchronous method call.</returns>
            [Fact]
            public static async Task ParallelDoAsyncShouldIterateAllItemsInCollection()
            {
                var fixture = new Fixture();
                var items = fixture.CreateMany<int>();
                var expected = items.Sum();

                int sum = 0;
                await items.ParallelDoAsync(async (x) =>
                {
                    sum += x;
                    await Task.FromResult(0);
                });

                Assert.Equal(expected, sum);
            }

            /// <summary>
            /// ParallelDoAsync() should throw when null sequence passed.
            /// </summary>
            [Fact]
            public static void ParallelDoAsyncShouldThrowWhenNullSequencePassed()
            {
                Action action = () => EnumerableExtensions.ParallelDoAsync<int>(null, null).Wait();

                var exception = Assert.Throws<AggregateException>(action);
                var inner = exception.InnerExceptions.Single() as ArgumentNullException;

                Assert.NotNull(inner);
                Assert.Equal("sequence", inner.ParamName);
            }

            /// <summary>
            /// ParallelDoAsync() should throw when null action passed.
            /// </summary>
            [Fact]
            public static void ParallelDoAsyncShouldThrowWhenNullActionPassed()
            {
                var fixture = new Fixture();
                var items = fixture.CreateMany<int>();

                Action action = () => EnumerableExtensions.ParallelDoAsync<int>(items, null).Wait();

                var exception = Assert.Throws<AggregateException>(action);
                var inner = exception.InnerExceptions.Single() as ArgumentNullException;

                Assert.NotNull(inner);
                Assert.Equal("action", inner.ParamName);
            }
        }

        /// <summary>
        /// Tests for Use() functions.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Justification = "Standard test pattern [JS]")]
        public static class UseFacts
        {
            /// <summary>
            /// Call dispose when using a sequence.
            /// </summary>
            /// <remarks>
            /// Note that disposed should be called exactly once after the object has been iterated over.
            /// i.e. Within the iteration dispose should not have been called.
            /// </remarks>
            [Fact]
            public static void CallDisposeWhenUsingASequence()
            {
                var disposableSequence = new List<IDisposable>();

                disposableSequence.Add(Substitute.For<IDisposable>());
                disposableSequence.Add(Substitute.For<IDisposable>());
                disposableSequence.Add(Substitute.For<IDisposable>());

                disposableSequence
                    .Use(x => x)
                    .Do(x => x.DidNotReceive().Dispose());

                disposableSequence
                    .Do(x => x.Received(1).Dispose());
            }

            /// <summary>
            /// Calls dispose when using a single object.
            /// </summary>
            [Fact]
            public static void CallDisposeWhenUsingASingleObject()
            {
                var disposable = Substitute.For<IDisposable>();

                disposable
                    .UseSingle()
                    .Do(x => x.DidNotReceive().Dispose());

                disposable
                    .Received(1).Dispose();
            }
        }
    }
}
