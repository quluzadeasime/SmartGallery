using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Core.Exceptions.AccountExceptions
{
    public class EmailIsNotConfirmedException : Exception
    {
        public EmailIsNotConfirmedException(string? message) : base(message) { }
    }
}
