using BassClefStudio.NET.Bots.Actions;
using BassClefStudio.NET.Bots.Content;
using BassClefStudio.NET.Bots.Helpers;
using BassClefStudio.NET.Bots.Inline;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BassClefStudio.NET.Bots.Services
{
    /// <summary>
    /// Represents a service which can send and recieve messages from users.
    /// </summary>
    public interface IBotService
    {
        /// <summary>
        /// A <see cref="bool"/> indicating whether the <see cref="IBotService"/> is currently connected.
        /// </summary>
        bool IsConnected { get; }

        /// <summary>
        /// Starts the <see cref="IBotService"/> and begins listening for communications.
        /// </summary>
        Task StartBotAsync();

        /// <summary>
        /// Stops the <see cref="IBotService"/> and closes communication connections.
        /// </summary>
        Task StopBotAsync();

        /// <summary>
        /// Sends a message through this <see cref="IBotService"/> through the current <see cref="IBotService"/>.
        /// </summary>
        /// <param name="message">The <see cref="IMessageContent"/> of the message to send.</param>
        /// <param name="chat">The <see cref="BotChat"/> to send the message to.</param>
        Task<bool> SendMessageAsync(IMessageContent message, BotChat chat);

        /// <summary>
        /// An event fired when a message is recieved from a user.
        /// </summary>
        event EventHandler<MessageReceivedEventArgs> MessageReceived;

        /// <summary>
        /// Updates the available <see cref="InlineCards"/> displayed to a user.
        /// </summary>
        /// <param name="response">The available inline queries in an <see cref="InlineCards"/> object.</param>
        Task<bool> UpdateInlineCardsAsync(InlineCards response);

        /// <summary>
        /// An event fired when an inline query is submitted by a user.
        /// </summary>
        event EventHandler<InlineQueryReceivedEventArgs> InlineQueryReceived;

        /// <summary>
        /// An event fired when a user initiates a callback through the <see cref="IBotService"/>.
        /// </summary>
        event EventHandler<CallbackReceivedEventArgs> CallbackReceived;
    }
}
