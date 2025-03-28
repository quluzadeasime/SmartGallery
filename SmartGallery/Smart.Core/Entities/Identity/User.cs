using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Core.Entities.Identity
{
    public class User : IdentityUser 
    {
        public bool IsResent { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public int? ConfirmationCode { get; set; }
        public string? PasswordResetToken { get; set; }
        public ICollection<Product> Products { get; set; }
        public DateTime? ConfirmationCodeSentAt { get; set; }
        public DateTime? PasswordResetTokenExpiry { get; set; }
    }
}
