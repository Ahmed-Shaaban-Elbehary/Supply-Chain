using System.ComponentModel;
using System.Net;

namespace SupplyChain.App.ViewModels
{
    public record ErrorResponse
    {
        private static string _message;
        private static string _innerMessage;
        private static string _stack;
        public string Message
        {
            get { return _message; }
            private set { _message = value; }
        }

        public string Stack
        {
            get { return _stack; }
            private set { _stack = value; }
        }

        public string InnerMessage
        {
            get { return _innerMessage; }
            private set { _innerMessage = value; }
        }

        public static ErrorResponse PreException(Exception exception)
        {
            _message = exception.Message;
            _stack = exception.StackTrace;
            if (exception.Message is null) _innerMessage = exception.InnerException.Message ?? "";
            else _innerMessage = exception.Message;
            var model = new ErrorResponse
            {
                Message = _message,
                Stack = _stack,
                InnerMessage = _innerMessage
            };
            return model;
        }
    }
}
