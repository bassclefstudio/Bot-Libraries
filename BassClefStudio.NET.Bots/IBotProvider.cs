using BassClefStudio.NET.Bots.Commands;
using BassClefStudio.NET.Bots.Inline;
using BassClefStudio.NET.Bots.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BassClefStudio.NET.Bots
{
    /// <summary>
    /// Represents a service configured to create a <see cref="Bot"/> with a specific configuration and purpose.
    /// </summary>
    public interface IBotProvider
    {
        /// <summary>
        /// The display-name for the <see cref="IBotProvider"/> and the attached <see cref="Bot"/> (see <see cref="Bot.BotName"/>).
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the <see cref="IBotService"/> this <see cref="IBotProvider"/> is built on.
        /// </summary>
        IBotService GetBotService();

        /// <summary>
        /// Gets a collection of <see cref="IBotCommand"/>s this bot supports.
        /// </summary>
        IEnumerable<IBotCommand> GetCommands();

        /// <summary>
        /// Gets a collection of <see cref="IInlineHandler"/>s this bot provides.
        /// </summary>
        IEnumerable<IInlineHandler> GetInlineHandlers();
    }
}
