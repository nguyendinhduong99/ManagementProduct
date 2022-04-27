using System.Globalization;

namespace PM.Common.Exception
{
    public class CustomException : System.Exception
    {
        public CustomException()
        {
        }

        public CustomException(string message) : base(message)
        {
        }

        public CustomException(string message, params object[] args) : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
