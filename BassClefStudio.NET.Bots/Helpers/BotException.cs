using System;
using System.Collections.Generic;
using System.Text;

namespace BassClefStudio.NET.Bots.Helpers
{
    public class BotException : Exception
    {
        public BotException() { }
        public BotException(string message) : base(message) { }
        public BotException(string message, Exception inner) : base(message, inner) { }
    }
}
