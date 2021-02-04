using BassClefStudio.NET.Bots.Services;
using BassClefStudio.NET.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace BassClefStudio.NET.Bots.Content
{
    /// <summary>
    /// Represents the content of a message sent between a <see cref="Bot"/> and a user.
    /// </summary>
    public interface IMessageContent
    {
        /// <summary>
        /// Set by the <see cref="IBotService"/> when managing messages, this is a unique <see cref="string"/> ID used to refer to the message after it's been sent or recieved.
        /// </summary>
        string Id { get; set; }
    }
}
