using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntiTamperAttribute.Exceptions
{
    public class HttpSecureHiddenInputException : Exception
    {
        public HttpSecureHiddenInputException(string message)
            : base(message)
        {
        }
    }
}
