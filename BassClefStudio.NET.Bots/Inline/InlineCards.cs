using BassClefStudio.NET.Bots.Actions;
using System;
using System.Collections.Generic;
using System.Text;

namespace BassClefStudio.NET.Bots.Inline
{
    /// <summary>
    /// Represents a collection of <see cref="IInlineCard"/>s and information about where and how to send them.
    /// </summary>
    public class InlineCards
    {
        /// <summary>
        /// The <see cref="IInlineQuery"/> query being responded to.
        /// </summary>
        public IInlineQuery Query { get; }

        /// <summary>
        /// The returned <see cref="IInlineCard"/>s.
        /// </summary>
        public IEnumerable<IInlineCard> Cards { get; }

        /// <summary>
        /// An <see cref="IBotAction"/> that can provide extra or additional information if requested by the user.
        /// </summary>
        public IBotAction MoreAction { get; }

        /// <summary>
        /// Creates a new <see cref="InlineCards"/>.
        /// </summary>
        /// <param name="query">The <see cref="IInlineQuery"/> query being responded to.</param>
        /// <param name="responseCards">The returned <see cref="IInlineCard"/>s.</param>
        /// <param name="moreAction">An <see cref="IBotAction"/> that can provide extra or additional information if requested by the user.</param>
        public InlineCards(IInlineQuery query, IEnumerable<IInlineCard> responseCards, IBotAction moreAction = null)
        {
            Query = query;
            Cards = responseCards;
            MoreAction = moreAction;
        }
    }
}
