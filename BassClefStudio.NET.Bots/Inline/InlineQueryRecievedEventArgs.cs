using BassClefStudio.NET.Bots.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace BassClefStudio.NET.Bots.Inline
{
    public class InlineQueryReceivedEventArgs
    {
        public IInlineQuery ReceivedQuery { get; }
        public IBotUser FromUser { get; }

        public InlineQueryReceivedEventArgs(IInlineQuery receivedQuery, IBotUser fromUser)
        {
            ReceivedQuery = receivedQuery;
            FromUser = fromUser;
        }
    }
}
