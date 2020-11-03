using BassClefStudio.NET.Bots.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace BassClefStudio.NET.Bots.Actions
{
    public class CallbackReceivedEventArgs : EventArgs
    {
        public CallbackBotAction CallbackAction { get; }
        public BotChat FromChat { get; }

        public CallbackReceivedEventArgs(CallbackBotAction action, BotChat fromChat)
        {
            CallbackAction = action;
            FromChat = fromChat;
        }
    }
}
