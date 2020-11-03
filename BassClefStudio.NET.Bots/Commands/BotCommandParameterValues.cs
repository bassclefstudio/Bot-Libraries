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
    public class BotCommandParameterValues
    {
        public string this[string name]
        {
            get
            {
                return ParameterValues.First(p => p.Item1.Name == name).Item2.Content;
            }
        }

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

        public BotCommandParameterValues(IBotCommand botCommand)
        {
            ParameterValues = new List<Tuple<BotCommandParameterInfo, BotCommandParameterValue>>();
            foreach (var param in botCommand.Parameters)
            {
                ParameterValues.Add(new Tuple<BotCommandParameterInfo, BotCommandParameterValue>(param, new BotCommandParameterValue()));
            }
        }

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

    public class BotCommandParameterValue
    {
        public string Content { get; set; }
        public BotCommandParameterValue()
        {
            Content = null;
        }
    }
}
