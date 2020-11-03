using System;
using System.Collections.Generic;
using System.Text;

namespace BassClefStudio.NET.Bots.Inline
{
    /// <summary>
    /// Represents a query made by an inline user reponse.
    /// </summary>
    public interface IInlineQuery
    {
        /// <summary>
        /// The text of the query.
        /// </summary>
        string QueryString { get; }
    }
}
