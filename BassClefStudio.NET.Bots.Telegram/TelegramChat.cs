using BassClefStudio.NET.Bots.Actions;
using BassClefStudio.NET.Bots.Content;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BassClefStudio.NET.Bots.Telegram
{
    /// <summary>
    /// Represents a <see cref="BotChat"/> created for the <see cref="TelegramBotService"/>.
    /// </summary>
    public class TelegramChat : BotChat
    {
        /// <inheritdoc/>
        public override string Id => ChatId.ToString();
        /// <summary>
        /// The ID of the chat in Telegram.
        /// </summary>
        public long ChatId { get; }

        /// <summary>
        /// Creates a new <see cref="TelegramChat"/>.
        /// </summary>
        /// <param name="chatId">The ID of the chat in Telegram.</param>
        public TelegramChat(long chatId)
        {
            ChatId = chatId;
        }
    }

    /// <summary>
    /// Represents an <see cref="IBotUser"/> for the <see cref="TelegramBotService"/>.
    /// </summary>
    public class TelegramUser : IBotUser
    {
        /// <inheritdoc/>
        public string Id => UserId.ToString();
        /// <summary>
        /// The ID of the user in Telegram.
        /// </summary>
        public int UserId { get; }

        /// <summary>
        /// Create a new <see cref="TelegramUser"/>
        /// </summary>
        /// <param name="userId">The ID of the user in Telegram.</param>
        public TelegramUser(int userId)
        {
            UserId = userId;
        }
    }
}
