using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Core.Exceptions.Commons
{
    public class LanguageSettings
    {
        public AccountErrorMessages AccountErrorMessages { get; set; }
    }

    public class AccountErrorMessages
    {
        public string UserLockOut { get; set; }
        public string UserTokenExpired { get; set; }
        public string EmailIsNotConfirmed { get; set; }
        public string ConfirmationNumberIsNotValid { get; set; }
        public string UsernameOrEmailAddressNotFound { get; set; }
    }
}
