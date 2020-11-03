using BassClefStudio.NET.Bots.Inline;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BassClefStudio.NET.Bots.Services
{
    /// <summary>
    /// A service that sends an <see cref="InlineCards"/> collection to an <see cref="IBotService"/>.
    /// </summary>
    /// <typeparam name="TService">The kind of <see cref="IBotService"/>s this <see cref="IBotInlineCardService{TService}"/> supports.</typeparam>
    public interface IBotInlineCardService<in TService> where TService : IBotService
    {
        /// <summary>
        /// Send the <see cref="InlineCards"/> response through the given <typeparamref name="TService"/>.
        /// </summary>
        /// <param name="service">The provided <see cref="IBotService"/>.</param>
        /// <param name="cards">The collection of <see cref="IInlineCard"/>s to send.</param>
        Task<bool> SendCardsAsync(TService service, InlineCards cards);

        /// <summary>
        /// Returns a <see cref="bool"/> indicating whether the given <see cref="InlineCards"/> collection can be sent with this <see cref="IBotInlineCardService{TService}"/>.
        /// </summary>
        /// <param name="cards">The <see cref="InlineCards"/> attempting to be sent.</param>
        bool CanSend(InlineCards cards);
    }
}
