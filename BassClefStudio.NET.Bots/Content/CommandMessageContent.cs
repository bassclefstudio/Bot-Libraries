using System;
using System.Collections.Generic;
using System.Text;

namespace BassClefStudio.NET.Bots.Content
{
    public class CommandMessageContent : IMessageContent
    {
        public string CommandName { get; }

        public CommandMessageContent(string name)
        {
            CommandName = name;
        }

        public override string ToString()
        {
            return $"\\{CommandName}";
        }
    }
}
