using BassClefStudio.NET.Bots.Content;
using BassClefStudio.NET.Bots.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace BassClefStudio.NET.Bots.Telegram.ContentServices
{
    internal class TelegramTextSendService : IBotSendService<TelegramBotService>
    {
        public bool CanSend(IMessageContent message)
        {
            return message is TextMessageContent;
        }

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
                    replyMarkup: new InlineKeyboardMarkup(
                        textMessage.Actions.Select(
                            a => InlineKeyboardButton.WithCallbackData(a.DisplayName, a.CallbackParameter))));

                chat.CurrentCallbackActions.AddRange(textMessage.Actions);
            }
            else
            {
                success = await service.BotClient.SendTextMessageAsync(
                    telegramChat.ChatId,
                    textMessage.Text);
            }

            return success != null;
        }
    }
}
