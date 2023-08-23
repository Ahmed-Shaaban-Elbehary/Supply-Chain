using System.ComponentModel;
using System.Net;

namespace SupplyChain.App.ViewModels
{
    public record ErrorResponse
    {

        private static string _message;
        public string Message
        {
            get { return _message; }
            private set { _message = value; }
        }
        
        public static ErrorResponse PreException(Exception exception)
        {
            _message = exception.Message;
            var model = new ErrorResponse
            {
                Message = _message
            };
            return model;
        }
    }
}
