using BassClefStudio.NET.Bots.Content;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BassClefStudio.NET.Bots.Services
{
    /// <summary>
    /// A service that edits/replaces existing messages through an <see cref="IBotService"/>.
    /// </summary>
    /// <typeparam name="TService">The type of <see cref="IBotService"/>s this <see cref="IBotEditService{TService}"/> supports.</typeparam>
    public interface IBotEditService<in TService> where TService : IBotService
    {
        /// <summary>
        /// Edits or replaces a message through the <typeparamref name="TService"/> to the given chat.
        /// </summary>
        /// <param name="service">The given <see cref="IBotService"/> to use to send the message.</param>
        /// <param name="message">The <see cref="IMessageContent"/> message to send.</param>
        /// <param name="chat">The <see cref="BotChat"/> to send the message to.</param>
        Task<bool> EditMessageAsync(TService service, IMessageContent message, BotChat chat);

        /// <summary>
        /// Returns a <see cref="bool"/> value indicating whether this <see cref="IBotEditService{TService}"/> can be used to edit the given <see cref="IMessageContent"/>.
        /// </summary>
        /// <param name="message">The <see cref="IMessageContent"/> being attempted to send.</param>
        bool CanEdit(IMessageContent message);
    }
}
