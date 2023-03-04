// ---------------------------------------------------------------------------
// <copyright file="XsdSupport.cs" company="Tethys">
//   Copyright (C) 2020-2023 T. Graf
// </copyright>
//
// Licensed under the Apache License, Version 2.0.
// Unless required by applicable law or agreed to in writing,
// software distributed under the License is distributed on an
// "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied.
// SPDX-License-Identifier: Apache-2.0
// ---------------------------------------------------------------------------


namespace Tethys.Xsd.Test
{
    using System.IO;
    using System.Text;
    using Logging;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class XsdSupportTest1
    {
        private const string XsdFilename = "test.xsd";
        private static readonly StringBuilder LogBuffer = new StringBuilder(1000);

        // <summary>
        // Test class initialization.
        // </summary>
        /// <param name="context">The context.</param>
        [ClassInitialize]
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Style", "IDE0060:Remove unused parameter", Justification = "Parameter is required")]
        public static void Initialize(TestContext context)
        {
            LogManager.Adapter = new StringLoggerFactoryAdapter(LogBuffer);
        }

        private static string CreateTestXsdFile()
        {
            const string XsdData =
                "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n"
                + "<xs:schema xmlns:xs=\"http://www.w3.org/2001/XMLSchema\">\r\n"
                + "  <xs:element name=\"tethys-document\">\r\n"
                + "  </xs:element>\r\n"
                + "</xs:schema>\r\n"
                + "";

            File.WriteAllText(XsdFilename, XsdData);

            return XsdFilename;
        }

        private bool HasErrorInLog()
        {
            return (LogBuffer.ToString().Contains("Validation error"));
        }

        [TestMethod]
        public void TestValidateXmlFile_Success()
        {
            const string XsdTestFilename = "test.xml";
            const string XmlData =
                "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n"
                + "<tethys-document>\r\n"
                + "</tethys-document>\r\n"
                + "";

            var xsdFile = CreateTestXsdFile();

            File.WriteAllText(XsdTestFilename, XmlData);

            XsdSupport.ValidateXmlFile(XsdTestFilename, xsdFile);

            Assert.IsFalse(this.HasErrorInLog());
        }

        [TestMethod]
        public void TestValidateXmlFile_Failure()
        {
            const string XsdTestFilename = "test.xml";
            const string XmlData =
                "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n"
                + "<invalid-document>\r\n"
                + "</invalid-document>\r\n"
                + "";

            var xsdFile = CreateTestXsdFile();

            File.WriteAllText(XsdTestFilename, XmlData);

            XsdSupport.ValidateXmlFile(XsdTestFilename, xsdFile);

            Assert.IsTrue(this.HasErrorInLog());
        }
    }
}
