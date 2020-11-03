using BassClefStudio.NET.Bots.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace BassClefStudio.NET.Bots.Inline
{
    /// <summary>
    /// Represents the content of the result of an <see cref="IInlineQuery"/>.
    /// </summary>
    public interface IInlineCard
    {
        /// <summary>
        /// The <see cref="IMessageContent"/> this <see cref="IInlineCard"/> creates when selected.
        /// </summary>
        IMessageContent Content { get; }
    }
}
