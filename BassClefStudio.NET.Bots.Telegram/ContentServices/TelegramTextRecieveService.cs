using BassClefStudio.NET.Bots.Content;
using BassClefStudio.NET.Bots.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace BassClefStudio.NET.Bots.Telegram.ContentServices
{
    internal class TelegramTextRecieveService : IBotRecieveService<Message>
    {
        /// <inheritdoc/>
        public bool CanConvert(Message message)
        {
            return message.Type == MessageType.Text;
        }

        /// <inheritdoc/>
        public IMessageContent ConvertMessage(Message message)
        {
            if (message.Text[0] == '/')
            {
                return new CommandMessageContent(message.Text.Substring(1));
            }
            else
            {
                return new TextMessageContent(message.Text);
            }
        }
    }
}
