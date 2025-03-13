using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Core.Exceptions.AccountExceptions
{
    public class UserLockOutException : Exception
    {
        public UserLockOutException(string? message) : base(message) { }
    }
}
