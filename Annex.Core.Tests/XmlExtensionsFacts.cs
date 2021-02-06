#region Copyright (c) 2015 James Snape
// <copyright file="XmlExtensionsFacts.cs" company="James Snape">
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
    using Annex.Core.Tests.Properties;
    using System;
    using System.Linq;
    using System.Xml.Linq;
    using Xunit;

    /// <summary>
    /// Xml Extensions Facts
    /// </summary>
    public class XmlExtensionsFacts
    {
        /// <summary>
        /// The document
        /// </summary>
        private readonly XDocument doc;

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlExtensionsFacts"/> class.
        /// </summary>
        public XmlExtensionsFacts()
        {
            this.doc = XDocument.Parse(Resources.XmlExtensionsFactsTestDocument);
        }

        /// <summary>
        /// Null throw.
        /// </summary>
        [Fact]
        public static void NullAncestorAttribute1NodeShouldThrow()
        {
            Action action = () => XmlExtensions.AncestorAttribute(null, null, null);
            var exception = Assert.Throws<ArgumentNullException>(action);
            Assert.Equal("node", exception.ParamName);
        }

        /// <summary>
        /// Null throw.
        /// </summary>
        [Fact]
        public static void NullAncestorAttribute2NodeShouldThrow()
        {
            Action action = () => XmlExtensions.AncestorAttribute(null, null, null, null);
            var exception = Assert.Throws<ArgumentNullException>(action);
            Assert.Equal("node", exception.ParamName);
        }

        /// <summary>
        /// Null throw.
        /// </summary>
        [Fact]
        public static void NullElementOrAncestorAttributeElementShouldThrow()
        {
            Action action = () => XmlExtensions.SelfOrAncestorAttribute(null, null, null);
            var exception = Assert.Throws<ArgumentNullException>(action);
            Assert.Equal("element", exception.ParamName);
        }

        /// <summary>
        /// Missing ancestor attribute should be null.
        /// </summary>
        [Fact]
        public void MissingAncestorAttributeShouldBeNull()
        {
            var leaf = this.doc.Descendants("LeafElement").Single();
            var value = leaf.AncestorAttribute("DoesntExist");
            Assert.Null(value);
        }

        /// <summary>
        /// Missing defaulted ancestor attribute should be default.
        /// </summary>
        [Fact]
        public void MissingDefaultedAncestorAttributeShouldBeDefault()
        {
            var leaf = this.doc.Descendants("LeafElement").Single();
            var defaultValue = "DefaultValue";
            var value = leaf.AncestorAttribute("DoesntExist", defaultValue);
            Assert.Equal(defaultValue, value);
        }

        /// <summary>
        /// Ancestor attribute should search upwards.
        /// </summary>
        [Fact]
        public void AncestorAttributeShouldSearchUpwards()
        {
            var leaf = this.doc.Descendants("LeafElement").Single();
            var value = leaf.AncestorAttribute("ChildAttribute");
            Assert.Equal("childAttribute1", value);
        }

        /// <summary>
        /// Filtered ancestor attribute should skip nonmatching elements.
        /// </summary>
        [Fact]
        public void FilteredAncestorAttributeShouldSkipNonmatchingElements()
        {
            var leaf = this.doc.Descendants("LeafElement").Single();
            var value = leaf.AncestorAttribute("RootElement", "ChildAttribute", null);
            Assert.Equal("childAttribute2", value);
        }

        /// <summary>
        /// Defaultediltered ancestor attribute should skip nonmatching elements.
        /// </summary>
        [Fact]
        public void DefaultedFilteredAncestorAttributeShouldSkipNonmatchingElements()
        {
            var leaf = this.doc.Descendants("LeafElement").Single();
            var defaultValue = "DefaultValue";
            var value = leaf.AncestorAttribute("RootElement", "MissingAttribute", defaultValue);
            Assert.Equal(defaultValue, value);
        }

        /// <summary>
        /// Element or ancestor attribute should start at element.
        /// </summary>
        [Fact]
        public void ElementAncestorAttributeShouldStartAtElement()
        {
            var leaf = this.doc.Descendants("LeafElement").Single();
            var value = leaf.SelfOrAncestorAttribute("LeafAttribute", null);
            Assert.Equal("leafattribute1", value);
        }

        /// <summary>
        /// Element or ancestor attribute should search upwards.
        /// </summary>
        [Fact]
        public void ElementAncestorAttributeShouldSearchUpwards()
        {
            var leaf = this.doc.Descendants("LeafElement").Single();
            var value = leaf.SelfOrAncestorAttribute("ChildAttribute", null);
            Assert.Equal("childAttribute1", value);
        }

        /// <summary>
        /// Defaulted element or ancestor attribute should return default if missing.
        /// </summary>
        [Fact]
        public void DefaultedElementAncestorAttributeShouldReturnDefaultIfMissing()
        {
            var leaf = this.doc.Descendants("LeafElement").Single();
            var defaultValue = "DefaultValue";
            var value = leaf.SelfOrAncestorAttribute("MissingAttribute", defaultValue);
            Assert.Equal(defaultValue, value);
        }
    }
}
