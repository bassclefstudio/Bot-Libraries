using BassClefStudio.NET.Bots.Content;
using BassClefStudio.NET.Bots.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BassClefStudio.NET.Bots.Commands
{
    /// <summary>
    /// Represents an <see cref="IBotCommand"/> that responds to a specific set of <see cref="string"/> command names.
    /// </summary>
    public abstract class TextBasedBotCommand : IBotCommand
    {
        /// <summary>
        /// The <see cref="string"/> commands this <see cref="TextBasedBotCommand"/> supports.
        /// </summary>
        public string[] CommandNames { get; }

        /// <inheritdoc/>
        public BotCommandParameterInfo[] Parameters { get; }

        /// <summary>
        /// Creates a new <see cref="TextBasedBotCommand"/>.
        /// </summary>
        /// <param name="commandNames">The <see cref="string"/> commands this <see cref="TextBasedBotCommand"/> supports.</param>
        /// <param name="parameters">An array of <see cref="BotCommandParameterInfo"/> containing information about the required <see cref="IBotCommand"/> parameters.</param>
        public TextBasedBotCommand(string[] commandNames, BotCommandParameterInfo[] parameters)
        {
            CommandNames = commandNames;
            Parameters = parameters;
        }

        /// <inheritdoc/>
        public bool CanExecute(CommandMessageContent message)
        {
            return CommandNames.Contains(message.CommandName);
        }

        /// <inheritdoc/>
        public abstract Task ExecuteAsync(Bot bot, BotChat context, BotCommandParameterValues commandParameters);
    }
}
