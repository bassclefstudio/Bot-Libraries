using BassClefStudio.NET.Bots.Content;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BassClefStudio.NET.Bots.Actions
{
    public interface IBotAction
    {
        string Id { get; }
        string DisplayName { get; }
        string Description { get; }
    }
}
