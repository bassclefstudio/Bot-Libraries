using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BassClefStudio.NET.Bots.Inline
{
    public interface IInlineHandler
    {
        bool CanHandle(IInlineQuery query);
        Task<InlineCards> GetCards(IInlineQuery query);
    }
}
