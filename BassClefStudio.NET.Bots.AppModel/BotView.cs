using BassClefStudio.AppModel.Navigation;
using BassClefStudio.NET.Bots.Actions;
using BassClefStudio.NET.Bots.Content;
using BassClefStudio.NET.Bots.Inline;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BassClefStudio.NET.Bots.AppModel
{
    /// <summary>
    /// Represents a <see cref="ConsoleView{T}"/> designed to simply feed output to the console from a provided <see cref="BotViewModel"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BotView<T> : ConsoleView<T> where T : BotViewModel
    {
        /// <inheritdoc/>
        public override async Task ShowView(object parameter)
        {
            if (!ViewModel.MyBot.IsConnected)
            {
                CreateTask("Connected");
                ViewModel.BotStarted += BotStarted;
                Write("MyBot: ");
                WriteLine("Bot is connecting...", ConsoleColor.Yellow);
                await AwaitTask("Connected");
            }

            WriteLine("Bot is ready.", ConsoleColor.Green);
            ViewModel.MyBot.MessageReceived += MessageReceived;
            ViewModel.MyBot.InlineQueryReceived += QueryReceived;
            ViewModel.MyBot.ActionInvoked += ActionInvoked;
            ViewModel.MyBot.UnauthorizedMessageReceived += UnauthorizedMessage;
            Console.ReadLine();
            WriteLine("Stopping bot...", ConsoleColor.Yellow);
            await ViewModel.CloseAsync();
        }

        private void UnauthorizedMessage(object sender, UnauthorizedMessageEventArgs e)
        {
            Write("Unauthorized user ");
            Write(e.User.Id, ConsoleColor.White);
            WriteLine(" attempted to access bot.");
        }

        private void ActionInvoked(object sender, ActionInvokedEventArgs e)
        {
            Write("Received ");
            Write($"{e.Invoked.DisplayName} ({e.Invoked.Id})", ConsoleColor.White);
            Write(" in chat ");
            WriteLine(e.ChatContext.Id, ConsoleColor.White);
        }

        private void MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            Write("Received ");
            Write(e.Message.ToString(), ConsoleColor.White);
            Write(" in chat ");
            WriteLine(e.ChatContext.Id, ConsoleColor.White);
        }

        private void QueryReceived(object sender, InlineQueryReceivedEventArgs e)
        {
            Write("Received ");
            Write(e.ReceivedQuery.ToString(), ConsoleColor.White);
            Write(" from user ");
            WriteLine(e.FromUser.Id, ConsoleColor.White);
        }

        private void BotStarted(object sender, EventArgs e)
        {
            CompleteTask("Connected");
        }
    }
}
