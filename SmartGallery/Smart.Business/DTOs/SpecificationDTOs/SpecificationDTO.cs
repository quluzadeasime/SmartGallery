using Smart.Business.DTOs.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Business.DTOs.SpecificationDTOs
{
    public class SpecificationDTO : BaseEntityDTO
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public int ProdcutId { get; set; }
    }
}
