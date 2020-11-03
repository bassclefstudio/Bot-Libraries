using BassClefStudio.NET.Bots.Content;
using BassClefStudio.NET.Bots.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.Payments;

namespace BassClefStudio.NET.Bots.Telegram.ContentServices
{
    public class TelegramParameterRequestSendService : IBotSendService<TelegramBotService>
    {
        public bool CanSend(IMessageContent message)
        {
            return message is ParameterRequestMessageContent;
        }

        public async Task<bool> SendMessageAsync(TelegramBotService service, IMessageContent message, BotChat chat)
        {
            var requestMessage = message as ParameterRequestMessageContent;
            var telegramChat = chat as TelegramChat;

            var sucess = await service.BotClient.SendTextMessageAsync(
                telegramChat.ChatId, 
                $"<b>{requestMessage.ParameterName}</b>\r\n{requestMessage.ParameterDescription}", 
                ParseMode.Html);
            
            return sucess != null;
        }
    }
}
