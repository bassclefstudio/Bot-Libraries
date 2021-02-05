using BassClefStudio.NET.Bots.Content;
using BassClefStudio.NET.Bots.Services;
using BassClefStudio.NET.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BassClefStudio.NET.Bots.Actions
{
    /// <summary>
    /// Represents an action presented to the user by the <see cref="Bot"/> that can prompt a response at a later time.
    /// </summary>
    public interface IBotAction : IIdentifiable<string>
    {
        /// <summary>
        /// The name of the action button, given to the user.
        /// </summary>
        string DisplayName { get; }

        /// <summary>
        /// A description of the <see cref="IBotAction"/>, sometimes shown to the user.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// A <see cref="bool"/> indicating whether the <see cref="IBotAction"/> should be removed from the <see cref="BotChat.ActiveActions"/> after it is invoked.
        /// </summary>
        bool OneTime { get; }

        /// <summary>
        /// Completes the <see cref="IBotAction"/>, indicating a user has invoked it.
        /// </summary>
        void Complete();
    }
}
