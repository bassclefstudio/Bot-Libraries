using BassClefStudio.NET.Bots.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace BassClefStudio.NET.Bots.Content
{
    public class MessageReceivedEventArgs : EventArgs
    {
        public IMessageContent ReceivedContent { get; }
        public BotChat ChatContext { get; }

        public MessageReceivedEventArgs(IMessageContent receivedContent, BotChat fromChat)
        {
            ReceivedContent = receivedContent;
            ChatContext = fromChat;
        }
    }
}
