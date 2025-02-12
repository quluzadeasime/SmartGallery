using Smart.Core.Entities.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Core.Entities
{
    public class Category : BaseEntity, IAuditedEntity
    {
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }

        // Base Fields
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
