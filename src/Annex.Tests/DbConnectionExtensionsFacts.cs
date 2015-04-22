﻿#region Copyright (c) 2015 James Snape
// <copyright file="DbConnectionExtensionsFacts.cs" company="James Snape">
// The MIT License (MIT)
//
// Copyright (c) 2015 James Snape
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
// </copyright>
#endregion

namespace Annex.Tests
{
    using System;
    using System.Data;
    using NSubstitute;
    using Xunit;

    /// <summary>
    /// DbConnectionExtensions Facts
    /// </summary>
    public class DbConnectionExtensionsFacts
    {
        /// <summary>
        /// The sample connection
        /// </summary>
        private IDbConnection connection = Substitute.For<IDbConnection>();

        /// <summary>
        /// Throws when null connection passed.
        /// </summary>
        [Fact]
        public static void ThrowsWhenNullConnectionPassed()
        {
            IDbConnection connection = null;
            Assert.Throws<ArgumentNullException>(() => connection.Connect());
        }

        /// <summary>
        /// Should open the connection.
        /// </summary>
        [Fact]
        public void ShouldOpenTheConnection()
        {
            this.connection.Connect();
            this.connection.Received().Open();
        }
    }
}
