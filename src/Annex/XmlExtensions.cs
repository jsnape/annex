#region Copyright (c) 2015 James Snape
// <copyright file="XmlExtensions.cs" company="James Snape">
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
    using System.Xml.Linq;

    /// <summary>
    /// Xml Extensions
    /// </summary>
    public static class XmlExtensions
    {
        /// <summary>
        /// Finds an attribute from an ancestor element.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="attributeName">The name.</param>
        /// <returns>The value of the attribute or null if not found.</returns>
        public static string AncestorAttribute(this XNode node, XName attributeName)
        {
            return node.AncestorAttribute(attributeName, null);
        }

        /// <summary>
        /// Finds an attribute from an ancestor element.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="attributeName">The attribute name.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>
        /// The value of the attribute or <c>defaultValue</c> if not found.
        /// </returns>
        public static string AncestorAttribute(this XNode node, XName attributeName, string defaultValue)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }

            return node
                .Ancestors()
                .Select(e => e.Attribute(attributeName))
                .Select(a => a == null ? null : a.Value)
                .Where(a => !string.IsNullOrEmpty(a))
                .FirstOrDefault() ?? defaultValue;
        }

        /// <summary>
        /// Finds an attribute from an ancestor element.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="elementName">Name of the element.</param>
        /// <param name="attributeName">The attribute name.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>
        /// The value of the attribute or <c>defaultValue</c> if not found.
        /// </returns>
        public static string AncestorAttribute(this XNode node, XName elementName, XName attributeName, string defaultValue)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }

            return node
                .Ancestors(elementName)
                .Select(e => e.Attribute(attributeName))
                .Select(a => a == null ? null : a.Value)
                .Where(a => !string.IsNullOrEmpty(a))
                .FirstOrDefault() ?? defaultValue;
        }

        /// <summary>
        /// Finds an element or ancestor attribute.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>
        /// The value of the attribute or <c>defaultValue</c> if not found.
        /// </returns>
        [Obsolete("Use SelfOrAncestorAttribute instead")]
        public static string ElementOrAncestorAttribute(this XElement element, XName attributeName, string defaultValue)
        {
            return element.SelfOrAncestorAttribute(attributeName, defaultValue);
        }

        /// <summary>
        /// Finds an element or ancestor attribute.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>
        /// The value of the attribute or <c>defaultValue</c> if not found.
        /// </returns>
        public static string SelfOrAncestorAttribute(this XElement element, XName attributeName, string defaultValue)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            var attribute = element.Attribute(attributeName);
            var value = attribute == null ? null : attribute.Value;

            if (string.IsNullOrEmpty(value))
            {
                value = element.AncestorAttribute(attributeName, defaultValue);
            }

            return value ?? defaultValue;
        }
    }
}
