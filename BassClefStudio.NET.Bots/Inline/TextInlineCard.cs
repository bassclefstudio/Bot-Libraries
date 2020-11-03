using BassClefStudio.NET.Bots.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace BassClefStudio.NET.Bots.Inline
{
    public class TextInlineCard : IInlineCard
    {
        public string Title { get; }
        public string Description { get; }

        public IMessageContent Content { get; }

        public TextInlineCard(string title, string description, IMessageContent content)
        {
            Title = title;
            Description = description;
            Content = content;
        }
    }
}
