using BassClefStudio.NET.Bots.Content;
using BassClefStudio.NET.Bots.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace BassClefStudio.NET.Bots.Telegram.ContentServices
{
    internal class TelegramTextEditService : IBotEditService<TelegramBotService>
    {
        /// <inheritdoc/>
        public bool CanEdit(IMessageContent message)
        {
            return message is TextMessageContent
                && !string.IsNullOrEmpty(message.Id);
        }
        
        /// <inheritdoc/>
        public async Task<bool> EditMessageAsync(TelegramBotService service, IMessageContent message, BotChat chat)
        {
            var textMessage = message as TextMessageContent;
            var telegramChat = chat as TelegramChat;

            Message success;
            if (textMessage.Actions != null && textMessage.Actions.Any())
            {
                success = await service.BotClient.EditMessageTextAsync(
                    new ChatId(telegramChat.ChatId),
                    int.Parse(message.Id),
                    textMessage.Text,
                    ParseMode.Html,
                    replyMarkup: new InlineKeyboardMarkup(
                        textMessage.Actions.Select(
                            a => InlineKeyboardButton.WithCallbackData(a.DisplayName, a.Id))));

                chat.ActiveActions.AddRange(textMessage.Actions);
            }
            else
            {
                success = await service.BotClient.EditMessageTextAsync(
                    new ChatId(telegramChat.ChatId),
                    int.Parse(message.Id),
                    textMessage.Text);
            }

            return success != null;
        }
    }
}
