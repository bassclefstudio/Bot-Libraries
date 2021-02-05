using BassClefStudio.NET.Bots.Content;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BassClefStudio.NET.Bots.Commands
{
    /// <summary>
    /// Represents a dictionary of <see cref="BotParameterRequest"/>s and their retreived values.
    /// </summary>
    public class BotParameters
    {
        /// <summary>
        /// Gets the value of the argument with the specified <see cref="string"/> key.
        /// </summary>
        /// <param name="key">The name of this argument (see <see cref="BotParameterRequest.Name"/>).</param>
        /// <returns>The <see cref="string"/> returned value, or 'null' if no value was set.</returns>
        public string this[string key]
        {
            get => Parameters.First(a => a.Key.Name == key).Value;
        }

        private Dictionary<BotParameterRequest, string> Parameters { get; }

        /// <summary>
        /// Creates a new <see cref="BotParameters"/> collection.
        /// </summary>
        /// <param name="infos">The collection of <see cref="BotParameterRequest"/>s describing the parameters to retreive.</param>
        public BotParameters(IEnumerable<BotParameterRequest> infos)
        {
            Parameters = new Dictionary<BotParameterRequest, string>();
            foreach (var info in infos)
            {
                Parameters.Add(info, null);
            }
        }

        /// <summary>
        /// Retreives values for every parameter in the <see cref="Parameters"/> collection asynchronously.
        /// </summary>
        /// <param name="bot">The <see cref="Bot"/> through which to send messages.</param>
        /// <param name="context">The <see cref="BotChat"/> to send the requests to.</param>
        public async Task GetParametersAsync(Bot bot, BotChat context)
        {
            foreach (var param in Parameters.Keys)
            {
                ParameterRequestMessageContent content = new ParameterRequestMessageContent(param.DisplayName, param.Description);
                await bot.SendMessageAsync(content, context);
                var response = await content.GetResponse;
                if (response is TextMessageContent textMessage)
                {
                    Parameters[param] = textMessage.Text;
                }
                else
                {
                    Debug.WriteLine($"Could not parse {response} as parameter.");
                }
            }
        }
    }
}
