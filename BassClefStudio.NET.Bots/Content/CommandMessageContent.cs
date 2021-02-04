using System;
using System.Collections.Generic;
using System.Text;

namespace BassClefStudio.NET.Bots.Content
{
    /// <summary>
    /// Represents the <see cref="IMessageContent"/> of a message that serves as a command.
    /// </summary>
    public class CommandMessageContent : IMessageContent
    {
        /// <summary>
        /// The name of the command requested.
        /// </summary>
        public string CommandName { get; }

        /// <inheritdoc/>
        public string Id { get; set; }

        /// <summary>
        /// Creates a new <see cref="CommandMessageContent"/>.
        /// </summary>
        /// <param name="name">The name of the command requested.</param>
        public CommandMessageContent(string name)
        {
            CommandName = name;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"\\{CommandName}";
        }
    }
}
