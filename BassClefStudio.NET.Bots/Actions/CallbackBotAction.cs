using BassClefStudio.NET.Bots.Content;
using BassClefStudio.NET.Bots.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BassClefStudio.NET.Bots.Actions
{
    public class CallbackBotAction : IBotAction
    {
        public string Id { get; }
        public string CallbackParameter { get; }
        public string DisplayName { get; }
        public string Description { get; }
        public bool OneTime { get; }

        public Func<Bot, BotChat, Task> InvokeTask { get; }

        public CallbackBotAction(string id, string parameter, string displayName, Action<Bot, BotChat> invokeAction, string description = null, bool oneTime = false)
            : this(id, parameter, displayName, (b, c) => Task.Run(() => invokeAction(b, c)), description, oneTime) { }
        public CallbackBotAction(string id, string parameter, string displayName, Func<Bot, BotChat, Task> invokeTask, string description = null, bool oneTime = false)
        {
            Id = id;
            CallbackParameter = parameter;
            DisplayName = displayName;
            Description = description;
            OneTime = oneTime;

            InvokeTask = invokeTask;
        }

        public async Task Invoke(Bot bot, BotChat context)
        {
            await InvokeTask(bot, context);
        }
    }
}
