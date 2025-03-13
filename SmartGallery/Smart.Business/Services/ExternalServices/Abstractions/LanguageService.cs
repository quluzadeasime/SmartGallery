using Microsoft.Extensions.Options;
using Smart.Business.Services.ExternalServices.Interfaces;
using Smart.Core.Exceptions.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Business.Services.ExternalServices.Abstractions
{
    public class LanguageService : ILanguageService
    {
        private readonly AccountErrorMessages _errorMessages;

        public LanguageService(IOptions<LanguageSettings> languageSettings)
        {
            _errorMessages = languageSettings.Value.AccountErrorMessages;
        }

        public AccountErrorMessages GetErrorMessages()
        {
            return _errorMessages;
        }
    }
}
