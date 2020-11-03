using BassClefStudio.NET.Bots.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BassClefStudio.NET.Bots.Helpers
{
    /// <summary>
    /// An exception thrown when <see cref="Bot"/>s or related services encounter an error.
    /// </summary>
    public class BotException : Exception
    {
        /// <inheritdoc/>
        public BotException() { }
        /// <inheritdoc/>
        public BotException(string message) : base(message) { }
        /// <inheritdoc/>
        public BotException(string message, Exception inner) : base(message, inner) { }
    }
}
