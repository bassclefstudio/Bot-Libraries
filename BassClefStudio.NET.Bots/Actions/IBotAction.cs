using BassClefStudio.NET.Bots.Content;
using BassClefStudio.NET.Bots.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BassClefStudio.NET.Bots.Actions
{
    /// <summary>
    /// Represents an action presented to the user by the <see cref="Bot"/> that can prompt a response at a later time.
    /// </summary>
    public interface IBotAction
    {
        /// <summary>
        /// A unique ID for the <see cref="IBotAction"/>.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// The name of the action button, given to the user.
        /// </summary>
        string DisplayName { get; }

        /// <summary>
        /// A description of the <see cref="IBotAction"/>, sometimes shown to the user.
        /// </summary>
        string Description { get; }
    }
}
