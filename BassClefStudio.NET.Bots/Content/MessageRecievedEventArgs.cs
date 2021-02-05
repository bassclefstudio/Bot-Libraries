using BassClefStudio.NET.Bots.Content;
using BassClefStudio.NET.Bots.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BassClefStudio.NET.Bots.Content
{
    /// <summary>
    /// An event called when an <see cref="IMessageContent"/> message is recieved by a <see cref="Bot"/>.
    /// </summary>
    public class MessageReceivedEventArgs : EventArgs
    {
        /// <summary>
        /// The recieved message content.
        /// </summary>
        public IMessageContent Message { get; }

        /// <summary>
        /// The <see cref="BotChat"/> this message was sent to.
        /// </summary>
        public BotChat ChatContext { get; }

        /// <summary>
        /// Creates a new <see cref="MessageReceivedEventArgs"/>.
        /// </summary>
        /// <param name="message">The recieved message content.</param>
        /// <param name="fromChat">The <see cref="BotChat"/> this message was sent to.</param>
        public MessageReceivedEventArgs(IMessageContent message, BotChat fromChat)
        {
            Message = message;
            ChatContext = fromChat;
        }
    }

    /// <summary>
    /// An event called when an <see cref="IMessageContent"/> message is recieved from an unauthorized user.
    /// </summary>
    public class UnauthorizedMessageEventArgs : EventArgs
    {
        /// <summary>
        /// The recieved message content.
        /// </summary>
        public IMessageContent Message { get; }

        /// <summary>
        /// The <see cref="IBotUser"/> who sent the message.
        /// </summary>
        public IBotUser User { get; }

        /// <summary>
        /// Creates a new <see cref="MessageReceivedEventArgs"/>.
        /// </summary>
        /// <param name="message">The recieved message content.</param>
        /// <param name="user">The <see cref="IBotUser"/> who sent the message.</param>
        public UnauthorizedMessageEventArgs(IMessageContent message, IBotUser user)
        {
            Message = message;
            User = user;
        }
    }
}
