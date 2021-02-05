using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BassClefStudio.NET.Bots.Content
{
    /// <summary>
    /// Represents an <see cref="IMessageContent"/> containing a file to send.
    /// </summary>
    public class FileMessageContent : IMessageContent
    {
        /// <inheritdoc/>
        public string Id { get; set; }

        /// <summary>
        /// A <see cref="Func{TResult}"/> that can get a readable <see cref="Stream"/> from which to pull the file data from.
        /// </summary>
        public Func<Stream> GetStream { get; }

        /// <summary>
        /// The name of the file being sent.
        /// </summary>
        public string FileName { get; }

        /// <summary>
        /// A human-readable description of what the file is.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Creates a new <see cref="FileMessageContent"/>.
        /// </summary>
        /// <param name="fileName">The name of the file being sent.</param>
        /// <param name="getStream">A <see cref="Func{TResult}"/> that can get a readable <see cref="Stream"/> from which to pull the file data from.</param>
        /// <param name="description">A human-readable description of what the file is.</param>
        public FileMessageContent(string fileName, Func<Stream> getStream, string description = null)
        {
            FileName = fileName;
            GetStream = getStream;
            Description = description;
        }
    }
}
