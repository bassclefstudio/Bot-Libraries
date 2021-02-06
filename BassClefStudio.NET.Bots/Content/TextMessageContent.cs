using BassClefStudio.NET.Bots.Actions;
using System;
using System.Collections.Generic;
using System.Text;

namespace BassClefStudio.NET.Bots.Content
{
    /// <summary>
    /// Represents the <see cref="IMessageContent"/> of a simple text message.
    /// </summary>
    public class TextMessageContent : IMessageContent
    {
        /// <summary>
        /// The content of the message.
        /// </summary>
        public string Text { get; set; }

        /// <inheritdoc/>
        public string Id { get; set; }

        /// <summary>
        /// A collection of <see cref="IBotAction"/>s that represent attached actionable buttons or responses.
        /// </summary>
        public List<IBotAction> Actions { get; }

        /// <summary>
        /// Creates a new <see cref="TextMessageContent"/> with a unique ID.
        /// </summary>
        /// <param name="text">The content of the message.</param>
        /// <param name="actions">A collection of <see cref="IBotAction"/>s that represent attached actionable buttons or responses.</param>
        public TextMessageContent(string text, params IBotAction[] actions)
        {
            Text = text;
            Actions = new List<IBotAction>(actions);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"\"{Text}\"";
        }
    }
}
