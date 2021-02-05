using BassClefStudio.NET.Bots.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BassClefStudio.NET.Bots.Content
{
    /// <summary>
    /// Represents the <see cref="IMessageContent"/> of a message sent to resolve a command parameter.
    /// </summary>
    public class ParameterRequestMessageContent : IMessageContent
    {
        /// <summary>
        /// The <see cref="BotParameterRequest"/> detailing the requested parameter.
        /// </summary>
        public BotParameterRequest Request { get; set; }

        /// <inheritdoc/>
        public string Id { get; set; }

        /// <summary>
        /// A task that returns the response <see cref="IMessageContent"/> at the point that it is received.
        /// </summary>
        public Task<IMessageContent> GetResponse => CompletionSource.Task;

        /// <summary>
        /// The backing <see cref="TaskCompletionSource{TResult}"/> for the <see cref="GetResponse"/> task.
        /// </summary>
        internal TaskCompletionSource<IMessageContent> CompletionSource { get; }

        /// <summary>
        /// Creates a new <see cref="ParameterRequestMessageContent"/>.
        /// </summary>
        /// <param name="request">The <see cref="BotParameterRequest"/> detailing the requested parameter.</param>
        public ParameterRequestMessageContent(BotParameterRequest request)
        {
            Request = request;
            CompletionSource = new TaskCompletionSource<IMessageContent>();
        }
    }
}
