using BassClefStudio.NET.Bots.Inline;
using System;
using System.Collections.Generic;
using System.Text;

namespace BassClefStudio.NET.Bots.Telegram
{
    /// <summary>
    /// Represets an <see cref="IInlineQuery"/> for the <see cref="TelegramBotService"/>.
    /// </summary>
    public class TelegramInlineQuery : IInlineQuery
    {
        /// <inheritdoc/>
        public string QueryString { get; }
        /// <summary>
        /// The <see cref="string"/> ID of the query in Telegram.
        /// </summary>
        public string QueryId { get; }

        /// <summary>
        /// Creates a new <see cref="TelegramInlineQuery"/>.
        /// </summary>
        /// <param name="queryString">The text of the <see cref="TelegramInlineQuery"/>.</param>
        /// <param name="id">The <see cref="string"/> ID of the query in Telegram.</param>
        public TelegramInlineQuery(string queryString, string id)
        {
            QueryString = queryString;
            QueryId = id;
        }
    }
}
