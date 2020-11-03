using BassClefStudio.NET.Bots.Actions;
using BassClefStudio.NET.Bots.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BassClefStudio.NET.Bots.Content
{
    /// <summary>
    /// Represents an identifiable conversation with a user (or a group of users).
    /// </summary>
    public abstract class BotChat
    {
        /// <summary>
        /// The ID of this <see cref="BotChat"/>.
        /// </summary>
        public abstract string Id { get; }

        /// <summary>
        /// A collection of <see cref="IMessageContent"/> objects representing the history of messages sent through this <see cref="BotChat"/>.
        /// </summary>
        public List<IMessageContent> MessageHistory { get; } = new List<IMessageContent>();

        /// <summary>
        /// A list of all current <see cref="CallbackBotAction"/>s that a <see cref="Bot"/> can respond to in this <see cref="BotChat"/>.
        /// </summary>
        public List<CallbackBotAction> CurrentCallbackActions { get; } = new List<CallbackBotAction>();
    }

    /// <summary>
    /// Represents the individual user in the bot framework, as opposed to the chat that they use to communicate (see <see cref="BotChat"/>).
    /// </summary>
    public interface IBotUser
    {
        /// <summary>
        /// The ID of the specific user.
        /// </summary>
        string Id { get; }
    }
}
