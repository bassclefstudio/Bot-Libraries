using BassClefStudio.NET.Bots.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace BassClefStudio.NET.Bots.Inline
{
    /// <summary>
    /// An event called when a request for <see cref="IInlineCard"/>s is received.
    /// </summary>
    public class InlineQueryReceivedEventArgs
    {
        /// <summary>
        /// The <see cref="IInlineQuery"/> query requesting <see cref="InlineCards"/>.
        /// </summary>
        public IInlineQuery ReceivedQuery { get; }

        /// <summary>
        /// The <see cref="IBotUser"/> that made the request. Different from the <see cref="BotChat"/> it was requested in, potentially.
        /// </summary>
        public IBotUser FromUser { get; }

        /// <summary>
        /// Creates a new <see cref="InlineQueryReceivedEventArgs"/>.
        /// </summary>
        /// <param name="receivedQuery">The <see cref="IInlineQuery"/> query requesting <see cref="InlineCards"/>.</param>
        /// <param name="fromUser">The <see cref="IBotUser"/> that made the request. Different from the <see cref="BotChat"/> it was requested in, potentially.</param>
        public InlineQueryReceivedEventArgs(IInlineQuery receivedQuery, IBotUser fromUser)
        {
            ReceivedQuery = receivedQuery;
            FromUser = fromUser;
        }
    }
}
