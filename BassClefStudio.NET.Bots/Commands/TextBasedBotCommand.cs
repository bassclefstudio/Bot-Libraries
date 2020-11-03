using BassClefStudio.NET.Bots.Content;
using BassClefStudio.NET.Bots.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BassClefStudio.NET.Bots.Commands
{
    public abstract class TextBasedBotCommand : IBotCommand
    {
        public string[] CommandNames { get; }
        public BotCommandParameterInfo[] Parameters { get; }

        public TextBasedBotCommand(string[] commandNames, BotCommandParameterInfo[] parameters)
        {
            CommandNames = commandNames;
            Parameters = parameters;
        }

        public bool CanExecute(CommandMessageContent message)
        {
            return CommandNames.Contains(message.CommandName);
        }

        public abstract Task Execute(Bot bot, BotChat context, BotCommandParameterValues commandParameters);
    }
}
