using Smart.Core.Entities.Commons;
using Smart.Core.Entities.Identity;
using Smart.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Core.Entities
{
    public class Transaction : BaseEntity, IAuditedEntity
    {
        public User User { get; set; }
        public long OrderId { get; set; }
        public string UserId { get; set; }
        public decimal Amount { get; set; }
        public string SessionId { get; set; }
        public string CheckToken { get; set; }
        public EOrderStatus Status { get; set; }
        public string? ResponseBody { get; set; }

        // Base Fields
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
