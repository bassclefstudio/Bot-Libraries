using BassClefStudio.NET.Bots.Content;
using BassClefStudio.NET.Bots.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

namespace BassClefStudio.NET.Bots.Telegram.ContentServices
{
    internal class TelegramFileSendService : IBotSendService<TelegramBotService>
    {
        /// <inheritdoc/>
        public bool CanSend(IMessageContent message)
        {
            return message is FileMessageContent;
        }

        /// <inheritdoc/>
        public async Task<bool> SendMessageAsync(TelegramBotService service, IMessageContent message, BotChat chat)
        {
            var fileMessage = message as FileMessageContent;
            var telegramChat = chat as TelegramChat;

            Message success;
            using (var fileStream = fileMessage.GetStream())
            {
                success = await service.BotClient.SendDocumentAsync(
                    telegramChat.ChatId,
                    new InputOnlineFile(fileStream, fileMessage.FileName),
                    fileMessage.Description,
                    ParseMode.Html);
            }

            if (success != null)
            {
                fileMessage.Id = success.MessageId.ToString();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
