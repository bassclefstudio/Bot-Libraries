using BassClefStudio.NET.Bots.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace BassClefStudio.NET.Bots.Actions
{
    /// <summary>
    /// An event called when an <see cref="IBotAction"/> presented to the user by a bot is invoked.
    /// </summary>
    public class ActionInvokedEventArgs : EventArgs
    {
        /// <summary>
        /// The <see cref="IBotAction"/> that was invoked by the user.
        /// </summary>
        public IBotAction Invoked { get; }

        /// <summary>
        /// The <see cref="BotChat"/> wherre the action was invoked.
        /// </summary>
        public BotChat ChatContext { get; }

        /// <summary>
        /// Creates a new <see cref="ActionInvokedEventArgs"/>.
        /// </summary>
        /// <param name="invoked">The <see cref="IBotAction"/> that was invoked by the user.</param>
        /// <param name="context">The <see cref="BotChat"/> wherre the action was invoked.</param>
        public ActionInvokedEventArgs(IBotAction invoked, BotChat context)
        {
            Invoked = invoked;
            ChatContext = context;
        }
    }
}
