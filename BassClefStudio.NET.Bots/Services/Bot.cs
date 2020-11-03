﻿using BassClefStudio.NET.Bots.Actions;
using BassClefStudio.NET.Bots.Commands;
using BassClefStudio.NET.Bots.Content;
using BassClefStudio.NET.Bots.Helpers;
using BassClefStudio.NET.Bots.Inline;
using BassClefStudio.NET.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BassClefStudio.NET.Bots.Services
{
    /// <summary>
    /// Represents a bot, an automated service that provides interaction between itself (or some back-end service) and a user using an <see cref="IBotService"/>.
    /// </summary>
    public class Bot
    {
        /// <summary>
        /// The name of this <see cref="Bot"/>.
        /// </summary>
        public string BotName { get; }

        private IBotService BotService { get; }
        /// <summary>
        /// A <see cref="bool"/> indicating whether the <see cref="Bot"/> is connected to the communication service.
        /// </summary>
        public bool IsConnected { get => BotService.IsConnected; }

        /// <summary>
        /// An event fired when a message is recieved from a user.
        /// </summary>
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;

        /// <summary>
        /// An event fired when an inline query is submitted by a user.
        /// </summary>
        public event EventHandler<InlineQueryReceivedEventArgs> InlineQueryReceived;

        /// <summary>
        /// An event fired when a user initiates a callback to the <see cref="Bot"/>.
        /// </summary>
        public event EventHandler<CallbackReceivedEventArgs> CallbackReceived;

        /// <summary>
        /// A collection of <see cref="IBotCommand"/>s representing the capabilities of the <see cref="Bot"/>.
        /// </summary>
        public IEnumerable<IBotCommand> Commands { get; }

        /// <summary>
        /// A collection of <see cref="IInlineHandler"/>s which represent inline features the <see cref="Bot"/> has while a user is in another chat.
        /// </summary>
        public IEnumerable<IInlineHandler> InlineHandlers { get; }

        /// <summary>
        /// Creates a new <see cref="Bot"/>.
        /// </summary>
        /// <param name="botName">The name of this <see cref="Bot"/>.</param>
        /// <param name="botService">An <see cref="IBotService"/> which will be used to send messages to and from the <see cref="Bot"/> and the user.</param>
        /// <param name="commands">A collection of <see cref="IBotCommand"/>s representing the capabilities of the <see cref="Bot"/>.</param>
        /// <param name="inlineHandlers">A collection of <see cref="IInlineHandler"/>s which represent inline features the <see cref="Bot"/> has while a user is in another chat.</param>
        public Bot(string botName, IBotService botService, IEnumerable<IBotCommand> commands, IEnumerable<IInlineHandler> inlineHandlers)
        {
            BotName = botName;
            BotService = botService;
            Commands = commands;
            InlineHandlers = inlineHandlers;
            BotService.MessageReceived += HandleMessage;
            BotService.CallbackReceived += HandleCallback;
            BotService.InlineQueryReceived += HandleQuery;
        }

        /// <summary>
        /// Creates a new <see cref="Bot"/> with no <see cref="BotName"/> set.
        /// </summary>
        /// <param name="botService">An <see cref="IBotService"/> which will be used to send messages to and from the <see cref="Bot"/> and the user.</param>
        /// <param name="commands">A collection of <see cref="IBotCommand"/>s representing the capabilities of the <see cref="Bot"/>.</param>
        /// <param name="inlineHandlers">A collection of <see cref="IInlineHandler"/>s which represent inline features the <see cref="Bot"/> has while a user is in another chat.</param>
        public Bot(IBotService botService, IEnumerable<IBotCommand> commands, IEnumerable<IInlineHandler> inlineHandlers)
            : this(null, botService, commands, inlineHandlers)
        { }

        /// <summary>
        /// Starts the <see cref="Bot"/> and begins reacting to user input.
        /// </summary>
        public async Task StartBotAsync() => await BotService.StartBotAsync();

        /// <summary>
        /// Stops the <see cref="Bot"/>, closing any connections to the service.
        /// </summary>
        public async Task StopBotAsync() => await BotService.StopBotAsync();

        /// <summary>
        /// Sends a message from the <see cref="Bot"/> through the current <see cref="IBotService"/>.
        /// </summary>
        /// <param name="content">The <see cref="IMessageContent"/> of the message to send.</param>
        /// <param name="chat">The <see cref="BotChat"/> to send the message to.</param>
        public async Task<bool> SendMessageAsync(IMessageContent content, BotChat chat)
        {
            var success = await BotService.SendMessageAsync(content, chat);
            if (success)
            {
                chat.MessageHistory.Add(content);
            }
            return success;
        }

        private void HandleMessage(object sender, MessageReceivedEventArgs e)
        {
            ProcessMessage(e.ReceivedContent, e.ChatContext);
            e.ChatContext.MessageHistory.Add(e.ReceivedContent);
            MessageReceived?.Invoke(this, e);
        }

        private void ProcessMessage(IMessageContent message, BotChat chat)
        {
            if (chat.MessageHistory.Any() && chat.MessageHistory.Last() is ParameterRequestMessageContent request)
            {
                request.ResultCallback(message);
            }
            else if (message is CommandMessageContent commandMessage)
            {
                var myCommand = Commands.FirstOrDefault(c => c.CanExecute(commandMessage));
                if (myCommand != null)
                {
                    var inputs = new BotCommandParameterValues(myCommand);
                    inputs.PopulateValues(this, chat, () => myCommand.Execute(this, chat, inputs));
                }
                else
                {
                    ////Command not found.
                }
            }
            else
            {
                ////Message not understood.
            }
        }

        private void HandleQuery(object sender, InlineQueryReceivedEventArgs e)
        {
            var queryTask = new SynchronousTask(() => ProcessQuery(e.ReceivedQuery), SynchronousTask.DefaultExceptionAction);
            _ = queryTask.RunTaskAsync();
            InlineQueryReceived?.Invoke(this, e);
        }

        private async Task ProcessQuery(IInlineQuery query)
        {
            var myHandler = InlineHandlers.FirstOrDefault(c => c.CanHandle(query));
            if(myHandler != null)
            {
                var cards = await myHandler.GetCards(query);
                await BotService.UpdateInlineCardsAsync(cards);
            }
            else
            {
                ////Handler not found.
            }
        }

        private void HandleCallback(object sender, CallbackReceivedEventArgs e)
        {
            SynchronousTask actionTask = new SynchronousTask(() => e.CallbackAction.Invoke(this, e.FromChat), SynchronousTask.DefaultExceptionAction);
            _ = actionTask.RunTaskAsync();

            CallbackReceived?.Invoke(this, e);
        }
    }
}