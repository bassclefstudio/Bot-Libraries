using BassClefStudio.NET.Bots.Actions;
using System;
using System.Collections.Generic;
using System.Text;

namespace BassClefStudio.NET.Bots.Content
{
    public class TextMessageContent : IMessageContent
    {
        public string Text { get; }
        public IEnumerable<CallbackBotAction> Actions { get; }

        public TextMessageContent(string text, IEnumerable<CallbackBotAction> actions = null)
        {
            Text = text;
            Actions = actions;
        }

        public override string ToString()
        {
            return $"\"{Text}\"";
        }
    }
}
