// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
// using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Security;
// using System.Security.Permissions;
using Microsoft.Xml.XPath;
// using Microsoft.Xml.Xsl.Runtime;

namespace Microsoft.Xml.Xsl
{
    using System;
    using Microsoft.Xml;

    /// <summary>
    /// Helper class implementing multiple overloads
    /// </summary>
    public abstract class XsltCommand
    {
        private static XmlReaderSettings s_readerSettings;

        static XsltCommand()
        {
            s_readerSettings = new XmlReaderSettings();
            s_readerSettings.DtdProcessing = DtdProcessing.Prohibit;
            s_readerSettings.ReadOnly = true;
        }

        /// <summary>
        /// Writer settings specified in the stylesheet
        /// </summary>
        public abstract XmlWriterSettings OutputSettings { get; }

        //------------------------------------------------
        // Transform methods which take an IXPathNavigable
        //------------------------------------------------

        public void Transform(IXPathNavigable input, XmlWriter results)
        {
            CheckArguments(input, results);
            Transform(input, (XsltArgumentList)null, results, new XmlUrlResolver());
        }

        public void Transform(IXPathNavigable input, XsltArgumentList arguments, XmlWriter results)
        {
            CheckArguments(input, results);
            Transform(input, arguments, results, new XmlUrlResolver());
        }

        public void Transform(IXPathNavigable input, XsltArgumentList arguments, TextWriter results)
        {
            CheckArguments(input, results);
            using (XmlWriter writer = XmlWriter.Create(results, OutputSettings))
            {
                Transform(input, arguments, writer, new XmlUrlResolver());
            }
        }

        public void Transform(IXPathNavigable input, XsltArgumentList arguments, Stream results)
        {
            CheckArguments(input, results);
            using (XmlWriter writer = XmlWriter.Create(results, OutputSettings))
            {
                Transform(input, arguments, writer, new XmlUrlResolver());
            }
        }

        //------------------------------------------------
        // Transform methods which take an XmlReader
        //------------------------------------------------

        public void Transform(XmlReader input, XmlWriter results)
        {
            CheckArguments(input, results);
            Transform(input, (XsltArgumentList)null, results, new XmlUrlResolver());
        }

        public void Transform(XmlReader input, XsltArgumentList arguments, XmlWriter results)
        {
            CheckArguments(input, results);
            Transform(input, arguments, results, new XmlUrlResolver());
        }

        public void Transform(XmlReader input, XsltArgumentList arguments, TextWriter results)
        {
            CheckArguments(input, results);
            using (XmlWriter writer = XmlWriter.Create(results, OutputSettings))
            {
                Transform(input, arguments, writer, new XmlUrlResolver());
            }
        }

        public void Transform(XmlReader input, XsltArgumentList arguments, Stream results)
        {
            CheckArguments(input, results);
            using (XmlWriter writer = XmlWriter.Create(results, OutputSettings))
            {
                Transform(input, arguments, writer, new XmlUrlResolver());
            }
        }

        //------------------------------------------------
        // Transform methods which take a uri
        //------------------------------------------------

        // [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings")]
        public void Transform(string inputUri, XmlWriter results)
        {
            CheckArguments(inputUri, results);
            using (XmlReader reader = XmlReader.Create(inputUri, s_readerSettings))
            {
                Transform(reader, (XsltArgumentList)null, results, new XmlUrlResolver());
            }
        }

        // [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings")]
        public void Transform(string inputUri, XsltArgumentList arguments, XmlWriter results)
        {
            CheckArguments(inputUri, results);
            using (XmlReader reader = XmlReader.Create(inputUri, s_readerSettings))
            {
                Transform(reader, arguments, results, new XmlUrlResolver());
            }
        }

        // [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings")]
        public void Transform(string inputUri, XsltArgumentList arguments, TextWriter results)
        {
            CheckArguments(inputUri, results);
            using (XmlReader reader = XmlReader.Create(inputUri, s_readerSettings))
            using (XmlWriter writer = XmlWriter.Create(results, OutputSettings))
            {
                Transform(reader, arguments, writer, new XmlUrlResolver());
            }
        }

        // [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings")]
        public void Transform(string inputUri, XsltArgumentList arguments, Stream results)
        {
            CheckArguments(inputUri, results);
            using (XmlReader reader = XmlReader.Create(inputUri, s_readerSettings))
            using (XmlWriter writer = XmlWriter.Create(results, OutputSettings))
            {
                Transform(reader, arguments, writer, new XmlUrlResolver());
            }
        }

        // [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings")]
        public void Transform(string inputUri, string resultsFile)
        {
            if (inputUri == null)
                throw new ArgumentNullException("inputUri");

            if (resultsFile == null)
                throw new ArgumentNullException("resultsFile");

            // SQLBUDT 276415: Prevent wiping out the content of the input file if the output file is the same
            using (XmlReader reader = XmlReader.Create(inputUri, s_readerSettings))
            using (XmlWriter writer = XmlWriter.Create(resultsFile, OutputSettings))
            {
                Transform(reader, (XsltArgumentList)null, writer, new XmlUrlResolver());
            }
        }

        //------------------------------------------------
        // Main Transform overloads
        //------------------------------------------------

        public abstract void Transform(XmlReader input, XsltArgumentList arguments, XmlWriter results, XmlResolver documentResolver);

        public abstract void Transform(IXPathNavigable input, XsltArgumentList arguments, XmlWriter results, XmlResolver documentResolver);

        //------------------------------------------------
        // Helper methods
        //------------------------------------------------

        private static void CheckArguments(object input, object results)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            if (results == null)
                throw new ArgumentNullException("results");
        }

        private static void CheckArguments(string inputUri, object results)
        {
            if (inputUri == null)
                throw new ArgumentNullException("inputUri");

            if (results == null)
                throw new ArgumentNullException("results");
        }
    }
}
