using Smart.Core.Entities.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Core.Entities
{
    public class Product : BaseEntity, IAuditedEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public ICollection<Specification> Specifications { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }
        public Stock Stock { get; set; }
        public string CreatedBy { get ; set ; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get ; set ; }
        public DateTime UpdatedOn { get ; set ; }
        public bool IsDeleted { get ; set ; }
    }
}
