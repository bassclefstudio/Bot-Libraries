using BassClefStudio.AppModel.Lifecycle;
using BassClefStudio.AppModel.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BassClefStudio.NET.Bots.AppModel
{
    /// <summary>
    /// An <see cref="IViewModel"/> designed for managing a single <see cref="Bot"/>.
    /// </summary>
    public abstract class BotViewModel : IViewModel, IActivationHandler
    {
        /// <inheritdoc/>
        public abstract bool Enabled { get; }

        /// <summary>
        /// An event that fires when the bot is connected.
        /// </summary>
        public event EventHandler BotStarted;

        /// <summary>
        /// The <see cref="Bot"/> instance this <see cref="BotViewModel"/> manages.
        /// </summary>
        public Bot MyBot { get; }

        /// <summary>
        /// The <see cref="App"/> instance of the AppModel app.
        /// </summary>
        public App MyApp { get; }

        private string BotName;
        /// <summary>
        /// Creates a new <see cref="BotViewModel"/> with the required services.
        /// </summary>
        /// <param name="botName">The name of the <see cref="Bot"/>.</param>
        /// <param name="myApp">The AppModel <see cref="App"/> to connect this <see cref="Bot"/> to.</param>
        /// <param name="myBot">The <see cref="Bot"/> this <see cref="IViewModel"/> will manage.</param>
        public BotViewModel(string botName, App myApp, Bot myBot)
        {
            MyBot = myBot;
            MyApp = myApp;
            BotName = botName;
        }

        /// <inheritdoc/>
        public void Activate(IActivatedEventArgs args)
        {
            MyBot.BotName = BotName;
        }

        /// <inheritdoc/>
        public bool CanHandle(IActivatedEventArgs args)
        {
            return args is LaunchActivatedEventArgs;
        }

        /// <inheritdoc/>
        public async Task InitializeAsync()
        {
            if (!MyBot.IsConnected)
            {
                await MyBot.StartBotAsync();
                BotStarted?.Invoke(this, new EventArgs());
            }
        }

        /// <summary>
        /// Stops the bot and closes the app.
        /// </summary>
        public async Task CloseAsync()
        {
            await MyBot.StopBotAsync();
            MyApp.Suspend();
        }
    }
}
