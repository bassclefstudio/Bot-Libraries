using BassClefStudio.NET.Bots.Actions;
using BassClefStudio.NET.Bots.Content;
using BassClefStudio.NET.Bots.Helpers;
using BassClefStudio.NET.Bots.Inline;
using BassClefStudio.NET.Bots.Services;
using BassClefStudio.NET.Bots.Telegram.ContentServices;
using BassClefStudio.NET.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace BassClefStudio.NET.Bots.Telegram
{
    /// <summary>
    /// An <see cref="IBotService"/> that supports the Telegram messaging platform.
    /// </summary>
    public class TelegramBotService : IBotService
    {
        #region TelegramProperties

        /// <summary>
        /// A <see cref="TelegramBotClient"/> that provides the back-end for Telegram communication.
        /// </summary>
        public TelegramBotClient BotClient { get; set; }

        /// <inheritdoc/>
        public bool IsConnected { get; private set; }

        /// <summary>
        /// A collection of <see cref="IBotSendService{TService}"/>s that support this <see cref="TelegramBotService"/>.
        /// </summary>
        public List<IBotSendService<TelegramBotService>> SendServices { get; }
        /// <summary>
        /// A collection of <see cref="IBotInlineCardService{TService}"/>s that support this <see cref="TelegramBotService"/>.
        /// </summary>
        public List<IBotInlineCardService<TelegramBotService>> InlineCardServices { get; }
        /// <summary>
        /// A collection of <see cref="IBotRecieveService{TMessage}"/>s that support this <see cref="TelegramBotService"/>.
        /// </summary>
        public List<IBotRecieveService<Message>> RecieveServices { get; }

        private string AccessToken;
        private TelegramUser[] KnownUsers;
        private List<TelegramChat> KnownChats;

        /// <summary>
        /// Creates a new <see cref="TelegramBotService"/>.
        /// </summary>
        /// <param name="options">A <see cref="TelegramBotOptions"/> object providing options and secrets for creating the connection.</param>
        /// <param name="sendServices">A collection of <see cref="IBotSendService{TService}"/>s that support this <see cref="TelegramBotService"/>.</param>
        /// <param name="recieveServices">A collection of <see cref="IBotRecieveService{TMessage}"/>s that support this <see cref="TelegramBotService"/>.</param>
        /// <param name="inlineCardServices">A collection of <see cref="IBotInlineCardService{TService}"/>s that support this <see cref="TelegramBotService"/>.</param>
        public TelegramBotService(TelegramBotOptions options, IEnumerable<IBotSendService<TelegramBotService>> sendServices = null, IEnumerable<IBotRecieveService<Message>> recieveServices = null, IEnumerable<IBotInlineCardService<TelegramBotService>> inlineCardServices = null)
        {
            KnownChats = new List<TelegramChat>();
            KnownUsers = options.KnownUsers.Select(u => new TelegramUser(u)).ToArray();
            AccessToken = options.AccessToken;
            SendServices = new List<IBotSendService<TelegramBotService>>(sendServices ?? new IBotSendService<TelegramBotService>[0]);
            RecieveServices = new List<IBotRecieveService<Message>>(recieveServices ?? new IBotRecieveService<Message>[0]);
            InlineCardServices = new List<IBotInlineCardService<TelegramBotService>>(inlineCardServices ?? new IBotInlineCardService<TelegramBotService>[0]);
            ////Add default services
            SendServices.Add(new TelegramTextSendService());
            SendServices.Add(new TelegramParameterRequestSendService());
            RecieveServices.Add(new TelegramTextRecieveService());
            InlineCardServices.Add(new TelegramTextInlineCardsService());
        }

        #endregion
        #region Lifecycle

        /// <inheritdoc/>
        public async Task StartBotAsync() => await Task.Run(() => StartBot());

        private void StartBot()
        {
            if (!IsConnected)
            {
                this.BotClient = new TelegramBotClient(AccessToken);

                this.BotClient.OnMessage += OnMessageReceived;
                this.BotClient.OnInlineQuery += OnInlineQuery;
                this.BotClient.OnCallbackQuery += OnCallback;
                this.BotClient.StartReceiving();
                IsConnected = true;
            }
        }

        /// <inheritdoc/>
        public async Task StopBotAsync() => await Task.Run(() => StopBot());

        private void StopBot()
        {
            if (this.IsConnected)
            {
                this.BotClient?.StopReceiving();
                this.BotClient.OnInlineQuery -= OnInlineQuery;
                this.BotClient.OnMessage -= OnMessageReceived;
                IsConnected = false;
            }
        }

        #endregion
        #region MessageReceived

        /// <inheritdoc/>
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;

        private void OnMessageReceived(object sender, MessageEventArgs e)
        {
            var fromUser = KnownUsers.FirstOrDefault(u => u.UserId == e.Message.From.Id);
            var message = RecieveServices.FirstOrDefault(r => r.CanConvert(e.Message))
                ?.ConvertMessage(e.Message);
            if (message != null && fromUser != null)
            {
                var chat = KnownChats.FirstOrDefault(c => c.ChatId == e.Message.Chat.Id);
                if(chat == null)
                {
                    chat = new TelegramChat(e.Message.Chat.Id);
                    KnownChats.Add(chat);
                }

                MessageReceived?.Invoke(
                    this,
                    new MessageReceivedEventArgs(message, chat));
            }
            else if (fromUser == null)
            {
                Debug.WriteLine($"Unauthorized message from {e.Message.From.Id}");
            }
        }

        #endregion
        #region Send

        /// <inheritdoc/>
        public async Task<bool> SendMessageAsync(IMessageContent message, BotChat chat)
        {
            if (KnownChats.Contains(chat))
            {
                var sendService = SendServices.FirstOrDefault(s => s.CanSend(message));
                if (sendService != null)
                {
                    return await sendService.SendMessageAsync(this, message, chat);
                }
                else
                {
                    ////No chat send service found.
                    return false;
                }
            }
            else
            {
                ////Chat unauthorized.
                return false;
            }
        }

        #endregion
        #region InlineReceieve

        /// <inheritdoc/>
        public event EventHandler<InlineQueryReceivedEventArgs> InlineQueryReceived;

        private void OnInlineQuery(object sender, InlineQueryEventArgs e)
        {
            var fromUser = KnownUsers.FirstOrDefault(u => u.UserId == e.InlineQuery.From.Id);
            if(fromUser != null)
            {
                InlineQueryReceived?.Invoke(this, new InlineQueryReceivedEventArgs(
                    new TelegramInlineQuery(e.InlineQuery.Query, e.InlineQuery.Id),
                    fromUser));
            }
        }

        #endregion
        #region InlineSend

        /// <inheritdoc/>
        public async Task<bool> UpdateInlineCardsAsync(InlineCards cards)
        {
            var inlineService = InlineCardServices.FirstOrDefault(s => s.CanSend(cards));
            if(inlineService != null)
            {
                return await inlineService.SendCardsAsync(this, cards);
            }
            else
            {
                ////No chat send service found.
                return false;
            }
        }

        #endregion
        #region Callback

        /// <inheritdoc/>
        public event EventHandler<CallbackReceivedEventArgs> CallbackReceived;

        private void OnCallback(object sender, CallbackQueryEventArgs e)
        {
            var fromChat = KnownChats.FirstOrDefault(c => c.ChatId == e.CallbackQuery.Message.Chat.Id);
            if (fromChat != null)
            {
                SynchronousTask answerTask = new SynchronousTask(() => BotClient.AnswerCallbackQueryAsync(e.CallbackQuery.Id, "Done!"));
                answerTask.RunTask();

                var action = fromChat.CurrentCallbackActions.FirstOrDefault(a => a.CallbackParameter == e.CallbackQuery.Data);
                if (action != null)
                {
                    CallbackReceived?.Invoke(
                        this,
                        new CallbackReceivedEventArgs(action, fromChat));

                    if (action.OneTime)
                    {
                        fromChat.CurrentCallbackActions.Remove(action);
                    }
                }
            }
        }

        #endregion
    }

    /// <summary>
    /// Represents options for creating a <see cref="TelegramBotService"/>.
    /// </summary>
    public class TelegramBotOptions
    {
        /// <summary>
        /// The access token for the bot connection.
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// An array of <see cref="int"/> user IDs of users that this bot can communicate with.
        /// </summary>
        public int[] KnownUsers { get; set; }
    }
}
