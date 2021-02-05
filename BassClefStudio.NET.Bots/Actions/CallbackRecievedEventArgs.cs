using BassClefStudio.NET.Bots.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace BassClefStudio.NET.Bots.Actions
{
    /// <summary>
    /// An event called when a <see cref="CallbackBotAction"/> action is activated by a user.
    /// </summary>
    public class CallbackReceivedEventArgs : EventArgs
    {
        /// <summary>
        /// The related <see cref="CallbackBotAction"/>.
        /// </summary>
        public CallbackBotAction CallbackAction { get; }

        /// <summary>
        /// The <see cref="BotChat"/> that sent the callback.
        /// </summary>
        public BotChat ChatContext { get; }

        /// <summary>
        /// Creates a new <see cref="CallbackReceivedEventArgs"/>.
        /// </summary>
        /// <param name="action">The related <see cref="CallbackBotAction"/>.</param>
        /// <param name="chatContext">The <see cref="BotChat"/> that sent the callback.</param>
        public CallbackReceivedEventArgs(CallbackBotAction action, BotChat chatContext)
        {
            CallbackAction = action;
            ChatContext = chatContext;
        }
    }
}
