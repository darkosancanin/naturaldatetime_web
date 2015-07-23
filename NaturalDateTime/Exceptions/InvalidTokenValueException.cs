using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalDateTime.Exceptions
{
    public class InvalidTokenValueException : ApplicationException
    {
        public InvalidTokenValueException(string message) : base(message)
        {
        }
    }
}
