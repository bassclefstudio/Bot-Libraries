using BassClefStudio.NET.Bots.Content;
using BassClefStudio.NET.Bots.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace BassClefStudio.NET.Bots.Telegram.ContentServices
{
    internal class TelegramTextSendService : IBotSendService<TelegramBotService>
    {
        /// <inheritdoc/>
        public bool CanSend(IMessageContent message)
        {
            return message is TextMessageContent;
        }

        /// <inheritdoc/>
        public async Task<bool> SendMessageAsync(TelegramBotService service, IMessageContent message, BotChat chat)
        {
            var textMessage = message as TextMessageContent;
            var telegramChat = chat as TelegramChat;

            Message success;
            if (textMessage.Actions != null && textMessage.Actions.Any())
            {
                success = await service.BotClient.SendTextMessageAsync(
                    telegramChat.ChatId,
                    textMessage.Text,
                    ParseMode.Html,
                    replyMarkup: new InlineKeyboardMarkup(
                        textMessage.Actions.Select(
                            a => InlineKeyboardButton.WithCallbackData(a.DisplayName, a.Id))));

                chat.ActiveActions.AddRange(textMessage.Actions);
            }
            else
            {
                success = await service.BotClient.SendTextMessageAsync(
                    telegramChat.ChatId,
                    textMessage.Text,
                    ParseMode.Html);
            }

            if (success != null)
            {
                textMessage.Id = success.MessageId.ToString();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
