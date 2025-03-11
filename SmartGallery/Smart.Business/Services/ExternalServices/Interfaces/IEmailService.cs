using Smart.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Business.Services.ExternalServices.Interfaces
{
    public interface IEmailService
    {
        Task SendMailMessageAsync(string toUserEmailAddress, User toUser, int confirmationNumber, string token);
    }
}
