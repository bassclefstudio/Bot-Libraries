using BassClefStudio.NET.Bots.Content;
using BassClefStudio.NET.Bots.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BassClefStudio.NET.Bots.Actions
{
    /// <summary>
    /// An <see cref="IBotAction"/> that uses <see cref="IBotService.CallbackReceived"/> to send responses back to the <see cref="Bot"/>.
    /// </summary>
    public class CallbackBotAction : IBotAction
    {
        /// <inheritdoc/>
        public string Id { get; }

        /// <summary>
        /// The name of the parameter in the callback.
        /// </summary>
        public string CallbackParameter { get; }

        /// <inheritdoc/>
        public string DisplayName { get; }

        /// <inheritdoc/>
        public string Description { get; }

        /// <summary>
        /// A <see cref="bool"/> indicating whether only one callback should be fired regardless of the number of times the callback is sent.
        /// </summary>
        public bool OneTime { get; }

        /// <summary>
        /// A <see cref="Task"/> created from the <see cref="Bot"/> and the <see cref="BotChat"/> this <see cref="CallbackBotAction"/> is sent to that is run when the button is clicked.
        /// </summary>
        public Func<Bot, BotChat, Task> InvokeTask { get; }

        /// <summary>
        /// Creates a new <see cref="CallbackBotAction"/>.
        /// </summary>
        /// <param name="id">A unique ID for the <see cref="IBotAction"/>.</param>
        /// <param name="parameter">The name of the parameter in the callback.</param>
        /// <param name="displayName">The name of the action button, given to the user.</param>
        /// <param name="invokeAction">A synchronous <see cref="Action"/> run with the given <see cref="Bot"/> and the <see cref="BotChat"/> this <see cref="CallbackBotAction"/> is sent to that is run when the button is clicked.</param>
        /// <param name="description">A description of the <see cref="IBotAction"/>, sometimes shown to the user.</param>
        /// <param name="oneTime">A <see cref="bool"/> indicating whether only one callback should be fired regardless of the number of times the callback is sent.</param>
        public CallbackBotAction(string id, string parameter, string displayName, Action<Bot, BotChat> invokeAction, string description = null, bool oneTime = false)
            : this(id, parameter, displayName, (b, c) => Task.Run(() => invokeAction(b, c)), description, oneTime) { }

        /// <summary>
        /// Creates a new <see cref="CallbackBotAction"/>.
        /// </summary>
        /// <param name="id">A unique ID for the <see cref="IBotAction"/>.</param>
        /// <param name="parameter">The name of the parameter in the callback.</param>
        /// <param name="displayName">The name of the action button, given to the user.</param>
        /// <param name="invokeTask">A <see cref="Task"/> created from the <see cref="Bot"/> and the <see cref="BotChat"/> this <see cref="CallbackBotAction"/> is sent to that is run when the button is clicked.</param>
        /// <param name="description">A description of the <see cref="IBotAction"/>, sometimes shown to the user.</param>
        /// <param name="oneTime">A <see cref="bool"/> indicating whether only one callback should be fired regardless of the number of times the callback is sent.</param>
        public CallbackBotAction(string id, string parameter, string displayName, Func<Bot, BotChat, Task> invokeTask, string description = null, bool oneTime = false)
        {
            Id = id;
            CallbackParameter = parameter;
            DisplayName = displayName;
            Description = description;
            OneTime = oneTime;

            InvokeTask = invokeTask;
        }

        /// <summary>
        /// An asynchronous <see cref="Task"/> run when the action is known to be invoked by the user (through the <see cref="IBotService"/>).
        /// </summary>
        /// <param name="bot">The <see cref="Bot"/> that recieved the request.</param>
        /// <param name="context">The <see cref="BotChat"/>/user that initiated the request.</param>
        public async Task Invoke(Bot bot, BotChat context)
        {
            await InvokeTask(bot, context);
        }
    }
}
