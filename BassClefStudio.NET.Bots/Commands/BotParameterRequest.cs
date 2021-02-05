using System;
using System.Collections.Generic;
using System.Text;

namespace BassClefStudio.NET.Bots.Commands
{
    /// <summary>
    /// Represents information required to request a parameter from the user.
    /// </summary>
    public class BotParameterRequest
    {
        /// <summary>
        /// The name of the parameter, used in the argument collection (see <see cref="IBotCommand.ExecuteAsync(Bot, Content.BotChat, BotParameters)"/>).
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The name of the <see cref="BotParameterRequest"/>, given to the user.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// A description of the <see cref="BotParameterRequest"/>, given to the user.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Creates a new <see cref="BotParameterRequest"/>.
        /// </summary>
        /// <param name="name">The name of the parameter, used in the <see cref="BotParameters"/> collection.</param>
        /// <param name="displayName">The name of the <see cref="BotParameterRequest"/>, given to the user.</param>
        /// <param name="description">A description of the <see cref="BotParameterRequest"/>, given to the user.</param>
        public BotParameterRequest(string name, string displayName, string description)
        {
            Name = name;

            DisplayName = displayName;
            Description = description;
        }
    }
}
