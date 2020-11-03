using BassClefStudio.NET.Bots.Actions;
using System;
using System.Collections.Generic;
using System.Text;

namespace BassClefStudio.NET.Bots.Inline
{
    public class InlineCards
    {
        public IInlineQuery Query { get; }
        public IEnumerable<IInlineCard> Cards { get; }
        public IBotAction MoreAction { get; }

        public InlineCards(IInlineQuery query, IEnumerable<IInlineCard> responseCards, IBotAction moreAction = null)
        {
            Query = query;
            Cards = responseCards;
            MoreAction = moreAction;
        }
    }
}
