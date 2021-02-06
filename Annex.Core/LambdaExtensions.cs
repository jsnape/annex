#region Copyright (c) 2014-2016 James Snape
// <copyright file="LambdaExtensions.cs" company="James Snape">
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
    using System.Linq.Expressions;
    using System.Reflection;

    /// <summary>
    /// Lambda Extensions
    /// </summary>
    public static class LambdaExtensions
    {
        /// <summary>
        /// Sets the property value.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="target">The target.</param>
        /// <param name="memberLambda">The member lamda.</param>
        /// <param name="value">The value.</param>
        /// <exception cref="ArgumentNullException">
        /// target is null
        /// memberLambda is null
        /// </exception>
        /// <exception cref="ArgumentException">
        /// memberLambda.Body cannot be null
        /// or
        /// memberLambda is not a property
        /// </exception>
        public static void SetPropertyValue<T>(this T target, LambdaExpression memberLambda, object value)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            if (memberLambda == null)
            {
                throw new ArgumentNullException(nameof(memberLambda));
            }

            if (!(memberLambda.Body is MemberExpression memberSelectorExpression))
            {
                throw new ArgumentException("memberLambda.Body cannot be null");
            }

            if (!(memberSelectorExpression.Member is PropertyInfo property))
            {
                throw new ArgumentException("memberLambda is not a property");
            }

            property.SetValue(target, value, null);
        }
    }
}
