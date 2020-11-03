using BassClefStudio.NET.Bots.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BassClefStudio.NET.Bots.Content
{
    public abstract class BotChat
    {
        public abstract string Id { get; }
        public List<IMessageContent> MessageHistory { get; } = new List<IMessageContent>();
        public List<CallbackBotAction> CurrentCallbackActions { get; } = new List<CallbackBotAction>();
    }

    public interface IBotUser
    {
        string Id { get; }
    }
}
