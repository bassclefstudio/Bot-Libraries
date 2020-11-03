using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BassClefStudio.NET.Bots.Content
{
    public class ParameterRequestMessageContent : IMessageContent
    {
        public string ParameterName { get; }
        public string ParameterDescription { get; }

        public Action<IMessageContent> ResultCallback { get; }

        public ParameterRequestMessageContent(string parameterName, string parameterDescription, Action<IMessageContent> resultCallback)
        {
            ParameterName = parameterName;
            ParameterDescription = parameterDescription;
            ResultCallback = resultCallback;
        }
    }
}
