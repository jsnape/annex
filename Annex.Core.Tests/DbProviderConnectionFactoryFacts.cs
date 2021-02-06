#region Copyright (c) 2015 James Snape
// <copyright file="DbProviderConnectionFactoryFacts.cs" company="James Snape">
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

// This depends on DbProviderFactories.GetFactory(provider) which isn't available in .NET Standard 2.0
#if NETFULL 

namespace Annex.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Xunit;

    /// <summary>
    /// DbProviderConnectionFactory Facts
    /// </summary>
    public static class DbProviderConnectionFactoryFacts
    {
        /// <summary>
        /// A sample provider
        /// </summary>
        private const string ValidProvider = "System.Data.SqlClient";

        /// <summary>
        /// A sample connection string
        /// </summary>
        private const string ValidConnectionString = "Data Source=(local);Integrated Security=SSPI;";

        /// <summary>
        /// Throws when created with null provider.
        /// </summary>
        [Fact]
        public static void ThrowsWhenCreatedWithNullProvider()
        {
            Assert.Throws<ArgumentNullException>(() => new DbProviderConnectionFactory(null, ValidConnectionString));
        }

        /// <summary>
        /// Throws when created with null connection string.
        /// </summary>
        [Fact]
        public static void ThrowsWhenCreatedWithNullConnectionString()
        {
            Assert.Throws<ArgumentNullException>(() => new DbProviderConnectionFactory(ValidProvider, null));
        }

        /// <summary>
        /// Throws when created with an invalid provider.
        /// </summary>
        [Fact]
        public static void ThrowsWhenCreatedWithInvalidProvider()
        {
            Assert.Throws<ArgumentException>(() => new DbProviderConnectionFactory("complete junk", ValidConnectionString));
        }

        /// <summary>
        /// Should create a connection with correct connection string.
        /// </summary>
        [Fact]
        public static void ShouldCreateConnectionWithCorrectConnectionString()
        {
            var factory = new DbProviderConnectionFactory(ValidProvider, ValidConnectionString);

            using (var connection = factory.CreateConnection())
            {
                Assert.Equal(ValidConnectionString, connection.ConnectionString);
            }
        }
    }
}
#endif
