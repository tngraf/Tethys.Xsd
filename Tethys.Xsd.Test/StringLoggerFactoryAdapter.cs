// --------------------------------------------------------------------------
// <copyright file="StringLoggerFactoryAdapter.cs" company="Tethys">
// Copyright  2009-2020 by Thomas Graf
//            All rights reserved.
//            Licensed under the Apache License, Version 2.0.
//            Unless required by applicable law or agreed to in writing,
//            software distributed under the License is distributed on an
//            "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
//            either express or implied.
// </copyright>
//
// ---------------------------------------------------------------------------

namespace Tethys.Xsd.Test
{
    using System;
    using System.Text;
    using Logging;

    /// <summary>
    /// Factory adapter to write a log output to a string.
    /// </summary>
    public class StringLoggerFactoryAdapter : ILoggerFactoryAdapter
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILog logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="StringLoggerFactoryAdapter" /> class.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        public StringLoggerFactoryAdapter(StringBuilder buffer)
        {
            this.logger = new StringLogger(buffer);
        } // StringLoggerFactoryAdapter()

        #region ILoggerFactoryAdapter Implementation
        /// <summary>
        /// Get a ILog instance by type.
        /// </summary>
        /// <param name="type">The type to use for the logger.</param>
        /// <returns>
        /// A logger.
        /// </returns>
        public ILog GetLogger(Type type)
        {
            return this.logger;
        } // GetLogger()

        /// <summary>
        /// Get a ILog instance by name.
        /// </summary>
        /// <param name="name">The name of the logger.</param>
        /// <returns>
        /// A logger.
        /// </returns>
        public ILog GetLogger(string name)
        {
            return this.logger;
        } // GetLogger()
        #endregion // ILoggerFactoryAdapter Implementation
    } // StringLoggerFactoryAdapter
}
