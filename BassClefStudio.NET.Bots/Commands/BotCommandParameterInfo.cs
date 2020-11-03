using System;
using System.Collections.Generic;
using System.Text;

namespace BassClefStudio.NET.Bots.Commands
{
    public class BotCommandParameterInfo
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }
        public string Description { get; set; }

        public BotCommandParameterInfo(string name, string displayName, string description)
        {
            Name = name;

            DisplayName = displayName;
            Description = description;
        }
    }
}
