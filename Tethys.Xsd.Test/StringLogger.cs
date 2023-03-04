// --------------------------------------------------------------------------
// <copyright file="StringLogger.cs" company="Tethys">
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

using System.Diagnostics.CodeAnalysis;

[module: SuppressMessage(
    "Microsoft.Design",
    "CA1020:AvoidNamespacesWithFewTypes",
    Scope = "namespace",
    Target = "Tethys.Logging.Simple",
    Justification = "Ok here.")]

namespace Tethys.Xsd.Test
{
    using System;
    using System.Text;
    using Logging;

    /// <summary>
    /// A simple logger that writes all output to Debug.WriteLine.
    /// </summary>
    /// <remarks>
    /// This class is based on ideas coming from
    /// <a href="http://netcommon.sourceforge.net">Common.Logging</a>.
    /// </remarks>
    public class StringLogger : AbstractLogger
    {
        private readonly StringBuilder buffer;

        /// <summary>
        /// Initializes a new instance of the <see cref="StringLogger"/> class.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        public StringLogger(StringBuilder buffer)
        {
            this.buffer = buffer;
        } // StringLogger()

        /// <summary>
        /// Do the actual logging by constructing the log message using a <see cref="StringBuilder" /> then
        /// sending the output to <c>Debug.WriteLine</c>.
        /// </summary>
        /// <param name="level">The <see cref="LogLevel" /> of the message.</param>
        /// <param name="message">The log message.</param>
        /// <param name="exception">An optional <see cref="Exception" /> associated with the message.</param>
        protected override void WriteInternal(LogLevel level, object message, Exception exception)
        {
            // Use a StringBuilder for better performance
            var sb = new StringBuilder();
            this.FormatOutput(sb, level, message, exception);

            this.buffer?.Append(sb.ToString());
        } // WriteInternal()
    } // DebugLogger
} // Tethys.Logging.Simple
