using BassClefStudio.NET.Bots.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace BassClefStudio.NET.Bots.Inline
{
    /// <summary>
    /// Represents an <see cref="IInlineCard"/> containing simple text information.
    /// </summary>
    public class TextInlineCard : IInlineCard
    {
        /// <summary>
        /// The title of the <see cref="TextInlineCard"/>.
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// The description of the <see cref="TextInlineCard"/> provided in the card content.
        /// </summary>
        public string Description { get; }

        /// <inheritdoc/>
        public IMessageContent Content { get; }

        /// <summary>
        /// Creates a new <see cref="TextInlineCard"/>.
        /// </summary>
        /// <param name="title">The title of the <see cref="IInlineCard"/>.</param>
        /// <param name="description">The description of the <see cref="TextInlineCard"/> provided in the card content.</param>
        /// <param name="content">The <see cref="IMessageContent"/> this <see cref="IInlineCard"/> creates when selected.</param>
        public TextInlineCard(string title, string description, IMessageContent content)
        {
            Title = title;
            Description = description;
            Content = content;
        }
    }
}
