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
    internal class TelegramParameterRequestSendService : IBotSendService<TelegramBotService>
    {
        /// <inheritdoc/>
        public bool CanSend(IMessageContent message)
        {
            return message is ParameterRequestMessageContent;
        }

        /// <inheritdoc/>
        public async Task<bool> SendMessageAsync(TelegramBotService service, IMessageContent message, BotChat chat)
        {
            var requestMessage = message as ParameterRequestMessageContent;
            var telegramChat = chat as TelegramChat;

            var success = await service.BotClient.SendTextMessageAsync(
                telegramChat.ChatId, 
                $"<b>{requestMessage.Request.DisplayName}</b>\r\n{requestMessage.Request.Description}", 
                ParseMode.Html);

            if (success != null)
            {
                requestMessage.Id = success.MessageId.ToString();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
