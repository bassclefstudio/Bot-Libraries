using BassClefStudio.NET.Bots.Content;
using BassClefStudio.NET.Bots.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BassClefStudio.NET.Bots.Commands
{
    public interface IBotCommand
    {
        BotCommandParameterInfo[] Parameters { get; }
        bool CanExecute(CommandMessageContent message);
        Task Execute(Bot bot, BotChat context, BotCommandParameterValues commandParameters);
    }
}