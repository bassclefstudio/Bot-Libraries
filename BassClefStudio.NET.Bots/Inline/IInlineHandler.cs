using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BassClefStudio.NET.Bots.Inline
{
    /// <summary>
    /// Represents a service that can provide <see cref="InlineCards"/> in response to certain <see cref="IInlineQuery"/>s.
    /// </summary>
    public interface IInlineHandler
    {
        /// <summary>
        /// Returns a <see cref="bool"/> value indicating whether the <see cref="IInlineQuery"/> can be handled by this <see cref="IInlineHandler"/>.
        /// </summary>
        /// <param name="query">The <see cref="IInlineQuery"/> to attempt to handle.</param>
        bool CanHandle(IInlineQuery query);

        /// <summary>
        /// Gets an <see cref="InlineCards"/> collection from the provided <see cref="IInlineQuery"/>.
        /// </summary>
        /// <param name="query">The given <see cref="IInlineQuery"/> query.</param>
        Task<InlineCards> GetCards(IInlineQuery query);
    }
}
