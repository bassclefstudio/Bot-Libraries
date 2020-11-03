using BassClefStudio.NET.Bots.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BassClefStudio.NET.Bots.Content
{
    /// <summary>
    /// Represents the <see cref="IMessageContent"/> of a message sent to resolve a <see cref="BotCommandParameterValue"/>.
    /// </summary>
    public class ParameterRequestMessageContent : IMessageContent
    {
        /// <summary>
        /// The name of the parameter to request. See <see cref="BotCommandParameterInfo.DisplayName"/>.
        /// </summary>
        public string ParameterName { get; }

        /// <summary>
        /// A description of the parameter to request. See <see cref="BotCommandParameterInfo.Description"/>.
        /// </summary>
        public string ParameterDescription { get; }

        /// <summary>
        /// An action to be run with the <see cref="IMessageContent"/> response when it is recieved.
        /// </summary>
        public Action<IMessageContent> ResultCallback { get; }

        /// <summary>
        /// Creates a new <see cref="ParameterRequestMessageContent"/>.
        /// </summary>
        /// <param name="parameterName">The name of the parameter to request.</param>
        /// <param name="parameterDescription">A description of the parameter to request.</param>
        /// <param name="resultCallback">An action to be run with the <see cref="IMessageContent"/> response when it is recieved.</param>
        public ParameterRequestMessageContent(string parameterName, string parameterDescription, Action<IMessageContent> resultCallback)
        {
            ParameterName = parameterName;
            ParameterDescription = parameterDescription;
            ResultCallback = resultCallback;
        }
    }
}
