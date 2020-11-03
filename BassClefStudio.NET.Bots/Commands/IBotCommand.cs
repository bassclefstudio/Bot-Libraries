using BassClefStudio.NET.Bots.Content;
using BassClefStudio.NET.Bots.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BassClefStudio.NET.Bots.Commands
{
    /// <summary>
    /// Represents a command or action that a <see cref="Bot"/> can execute with given <see cref="BotCommandParameterValues"/> from the user.
    /// </summary>
    public interface IBotCommand
    {
        /// <summary>
        /// An array of <see cref="BotCommandParameterInfo"/> containing information about the required <see cref="IBotCommand"/> parameters.
        /// </summary>
        BotCommandParameterInfo[] Parameters { get; }

        /// <summary>
        /// A <see cref="bool"/> indicating whether this <see cref="IBotCommand"/> is referred to by the provided message.
        /// </summary>
        /// <param name="message">A <see cref="CommandMessageContent"/> message.</param>
        bool CanExecute(CommandMessageContent message);

        /// <summary>
        /// Executes the <see cref="IBotCommand"/> asynchronously.
        /// </summary>
        /// <param name="bot">The <see cref="Bot"/> executing the command.</param>
        /// <param name="context">The <see cref="BotChat"/> that requested the <see cref="IBotCommand"/>.</param>
        /// <param name="commandParameters">A <see cref="BotCommandParameterValues"/> collection with the required command values.</param>
        Task ExecuteAsync(Bot bot, BotChat context, BotCommandParameterValues commandParameters);
    }
}