using System;
using System.Collections.Generic;
using System.Text;

namespace BassClefStudio.NET.Bots.Commands
{
    /// <summary>
    /// Represents information required to request a <see cref="BotCommandParameterValue"/> from the user.
    /// </summary>
    public class BotCommandParameterInfo
    {
        /// <summary>
        /// The name of the parameter, used in the <see cref="BotCommandParameterValues"/> collection.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The name of the <see cref="BotCommandParameterInfo"/>, given to the user.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// A description of the <see cref="BotCommandParameterInfo"/>, given to the user.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Creates a new <see cref="BotCommandParameterInfo"/>.
        /// </summary>
        /// <param name="name">The name of the parameter, used in the <see cref="BotCommandParameterValues"/> collection.</param>
        /// <param name="displayName">The name of the <see cref="BotCommandParameterInfo"/>, given to the user.</param>
        /// <param name="description">A description of the <see cref="BotCommandParameterInfo"/>, given to the user.</param>
        public BotCommandParameterInfo(string name, string displayName, string description)
        {
            Name = name;

            DisplayName = displayName;
            Description = description;
        }
    }
}
