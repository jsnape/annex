#region Copyright (c) 2015 James Snape
// <copyright file="DbProviderConnectionFactory.cs" company="James Snape">
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
namespace Annex
{
    using System;
    using System.Data;
    using System.Data.Common;
    using System.Diagnostics;

    /// <summary>
    /// DbProvider Connection Factory
    /// </summary>
    public class DbProviderConnectionFactory : IConnectionFactory
    {
        /// <summary>
        /// The factory
        /// </summary>
        private readonly DbProviderFactory factory;

        /// <summary>
        /// The connection string
        /// </summary>
        private readonly string connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbProviderConnectionFactory"/> class.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="connectionString">The connection string.</param>
        public DbProviderConnectionFactory(string provider, string connectionString)
        {
            if (string.IsNullOrEmpty(provider))
            {
                throw new ArgumentNullException("provider");
            }

            this.factory = DbProviderFactories.GetFactory(provider);
            Debug.Assert(this.factory != null, "The factory throws if invalid provider passed.");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException("connectionString");
            }

            this.connectionString = connectionString;
        }

        /// <summary>
        /// Creates a connection.
        /// </summary>
        /// <returns>A connection instance.</returns>
        public IDbConnection CreateConnection()
        {
            var connection = this.factory.CreateConnection();
            connection.ConnectionString = this.connectionString;

            return connection;
        }
    }
}
#endif
