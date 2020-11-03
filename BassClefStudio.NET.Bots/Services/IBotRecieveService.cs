using BassClefStudio.NET.Bots.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace BassClefStudio.NET.Bots.Services
{
    /// <summary>
    /// A service that converts recieved messages of type <typeparamref name="TMessage"/> to <see cref="IMessageContent"/> objects (used in <see cref="IBotService"/> and <see cref="Bot"/>s).
    /// </summary>
    /// <typeparam name="TMessage">The type of input message to convert.</typeparam>
    public interface IBotRecieveService<in TMessage>
    {
        /// <summary>
        /// Recieve and convert the given <typeparamref name="TMessage"/> to an <see cref="IMessageContent"/>.
        /// </summary>
        /// <param name="message">The <typeparamref name="TMessage"/> to convert.</param>
        IMessageContent ConvertMessage(TMessage message);

        /// <summary>
        /// Returns a <see cref="bool"/> value indicating whether this <see cref="IBotRecieveService{TMessage}"/> can be used to convert the given <typeparamref name="TMessage"/>.
        /// </summary>
        /// <param name="message">The <typeparamref name="TMessage"/> being attempted to convert.</param>
        bool CanConvert(TMessage message);
    }
}
