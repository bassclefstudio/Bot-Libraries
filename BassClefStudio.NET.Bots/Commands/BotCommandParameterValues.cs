using BassClefStudio.NET.Bots.Content;
using BassClefStudio.NET.Bots.Helpers;
using BassClefStudio.NET.Bots.Services;
using BassClefStudio.NET.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BassClefStudio.NET.Bots.Commands
{
    /// <summary>
    /// Represents an object that can resolve and provide a keyed collection of <see cref="BotCommandParameterValue"/>s from a <see cref="BotCommandParameterInfo"/> collection.
    /// </summary>
    public class BotCommandParameterValues
    {
        /// <summary>
        /// Gets the <see cref="string"/> content of the <see cref="BotCommandParameterValue"/> attached to the given <see cref="BotCommandParameterInfo.Name"/>.
        /// </summary>
        /// <param name="name">The <see cref="string"/> name of the value.</param>
        public string this[string name]
        {
            get
            {
                return ParameterValues.First(p => p.Item1.Name == name).Item2.Content;
            }
        }

        /// <summary>
        /// Gets the <see cref="string"/> content of the <see cref="BotCommandParameterValue"/> attached to the given <see cref="BotCommandParameterInfo"/>.
        /// </summary>
        /// <param name="parameterInfo">A <see cref="BotCommandParameterInfo"/> containing information about the value to retrieve.</param>
        public string this[BotCommandParameterInfo parameterInfo]
        {
            get
            {
                return ParameterValues.First(p => p.Item1 == parameterInfo).Item2.Content;
            }
            set
            {
                ParameterValues.First(p => p.Item1 == parameterInfo).Item2.Content = value;
            }
        }

        private List<Tuple<BotCommandParameterInfo, BotCommandParameterValue>> ParameterValues;

        /// <summary>
        /// Creates a new <see cref="BotCommandParameterValues"/> for the given <see cref="IBotCommand"/>.
        /// </summary>
        /// <param name="botCommand">The <see cref="IBotCommand"/> to retrieve and build the collection of <see cref="BotCommandParameterValue"/>s for.</param>
        public BotCommandParameterValues(IBotCommand botCommand)
        {
            ParameterValues = new List<Tuple<BotCommandParameterInfo, BotCommandParameterValue>>();
            foreach (var param in botCommand.Parameters)
            {
                ParameterValues.Add(new Tuple<BotCommandParameterInfo, BotCommandParameterValue>(param, new BotCommandParameterValue()));
            }
        }

        /// <summary>
        /// Populates the colllection of <see cref="BotCommandParameterValue"/>s by requesting information asynchronously from the user and setting up callbacks.
        /// </summary>
        /// <param name="bot">The <see cref="Bot"/> to request the information with.</param>
        /// <param name="context">The <see cref="BotChat"/> user to request information from.</param>
        /// <param name="commandCallback">A <see cref="Func{TResult}"/> returning a <see cref="Task"/> that is asynchronously run when all parameters have been resolved.</param>
        public void PopulateValues(Bot bot, BotChat context, Func<Task> commandCallback)
        {
            Debug.WriteLine("Starting to populate values...");
            if (ParameterValues.Any())
            {
                SynchronousTask populateTask = new SynchronousTask(() => PopulateValue(bot, context, 0, commandCallback), SynchronousTask.DefaultExceptionAction);
                _ = populateTask.RunTaskAsync();
            }
            else
            {
                Debug.WriteLine("No values found. Running command...");
                SynchronousTask callbackTask = new SynchronousTask(commandCallback, SynchronousTask.DefaultExceptionAction);
                _ = callbackTask.RunTaskAsync();
            }
        }

        private async Task PopulateValue(Bot bot, BotChat context, int index, Func<Task> commandCallback)
        {
            Debug.WriteLine($"Getting parameter value {index}");

            var parameterInfo = ParameterValues[index].Item1;
            if (index == ParameterValues.Count - 1)
            {
                await bot.SendMessageAsync(new ParameterRequestMessageContent(parameterInfo.DisplayName, parameterInfo.Description, m => ValueCallback(m, parameterInfo, commandCallback)), context);
            }
            else
            {
                await bot.SendMessageAsync(new ParameterRequestMessageContent(parameterInfo.DisplayName, parameterInfo.Description, m => ValueCallback(m, parameterInfo, () => PopulateValue(bot, context, index + 1, commandCallback))), context);
            }
        }

        private void ValueCallback(IMessageContent message, BotCommandParameterInfo parameterInfo, Func<Task> callback)
        {
            Debug.WriteLine($"Received command parameter {parameterInfo.Name}");

            if (message is TextMessageContent textMessage)
            {
                this[parameterInfo] = textMessage.Text;
            }
            else
            {
                ////Value could not be parsed. Leaving as null...
            }

            Debug.WriteLine($"Starting callback...");

            SynchronousTask callbackTask = new SynchronousTask(callback, SynchronousTask.DefaultExceptionAction);
            _ = callbackTask.RunTaskAsync();
        }
    }

    /// <summary>
    /// Represents the result of a parameter request in an <see cref="IBotCommand"/>.
    /// </summary>
    public class BotCommandParameterValue
    {
        /// <summary>
        /// The content of the <see cref="BotCommandParameterValue"/> as a <see cref="string"/>.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Creates a new <see cref="BotCommandParameterValue"/>.
        /// </summary>
        /// <param name="content">The content of the <see cref="BotCommandParameterValue"/>. Defaults to null.</param>
        public BotCommandParameterValue(string content = null)
        {
            Content = content;
        }
    }
}
