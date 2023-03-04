// ---------------------------------------------------------------------------
// <copyright file="XsdSupport.cs" company="Tethys">
//   Copyright (C) 2014-2023 T. Graf
// </copyright>
//
// Licensed under the Apache License, Version 2.0.
// Unless required by applicable law or agreed to in writing,
// software distributed under the License is distributed on an
// "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied.
// SPDX-License-Identifier: Apache-2.0
// ---------------------------------------------------------------------------

namespace Tethys.Xsd
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Xml;
    using System.Xml.Schema;

    using Tethys.Logging;

    /// <summary>
    /// Support for XML Schema (XSD) processing.
    /// </summary>
    public class XsdSupport
    {
        #region PRIVATE PROPERTIES
        /// <summary>
        /// The logger for this class.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(XsdSupport));
        #endregion // PRIVATE PROPERTIES

        //// ---------------------------------------------------------------------

        #region PUBLIC METHODS
        /// <summary>
        /// Validates the specified XML file.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="schemaFile">The schema file.</param>
        public static void ValidateXmlFile(string filename, string schemaFile)
        {
            // Set the validation settings.
            var settings = new XmlReaderSettings();
            settings.ValidationType = ValidationType.Schema;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessInlineSchema;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
            settings.ValidationEventHandler += ValidationCallBack;

            settings.CheckCharacters = true;

            var schema = ReadXmlSchema(schemaFile);
            if (schema == null)
            {
                // got no valid schema ...
                return;
            } // if

            // Parse the file.
            XmlReader reader = null;
            try
            {
                settings.Schemas.Add(schema);
                reader = XmlReader.Create(filename, settings);

                Log.Info("Starting validation ...");
                while (reader.Read())
                {
                    // intentionally empty!
                } // while

                Log.Info("Validation done.");
            }
            catch (XmlException xmlex)
            {
                Log.Error("XML parsing error", xmlex);
            }
            catch (Exception ex)
            {
                Log.Error("XML parsing error", ex);
            }
            finally
            {
                reader?.Close();
            } // finally
        } // ValidateXmlFile()
        #endregion // PUBLIC METHODS

        //// ---------------------------------------------------------------------

        #region PRIVATE METHODS
        /// <summary>
        /// Reads an XML schema form the specified file.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns>A <see cref="XmlSchema"/>.</returns>
        private static XmlSchema ReadXmlSchema(string filename)
        {
            var fi = new FileInfo(filename);
            StreamReader sr = null;
            XmlSchema schema = null;
            try
            {
                sr = new StreamReader(fi.FullName);
                schema = XmlSchema.Read(sr, ValidationCallBack);
            }
            catch (Exception ex)
            {
                Log.Error("Error reading schema file", ex);
            }
            finally
            {
                sr?.Close();
            } // finally

            return schema;
        } // ReadXmlSchema()

        /// <summary>
        /// Occurs when the reader encounters validation errors.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="ValidationEventArgs"/> instance
        /// containing the event data.</param>
        private static void ValidationCallBack(object sender, ValidationEventArgs args)
        {
            if (args.Severity == XmlSeverityType.Warning)
            {
                Log.WarnFormat(
                    CultureInfo.CurrentCulture,
                    "Matching schema not found. No validation occurred: {0}",
                    args.Message);
            }
            else
            {
                Log.Error($"Validation error: {args.Message}");
            } // if
        } // ValidationCallBack()
        #endregion // PRIVATE METHODS
    } // XsdSupport
}
