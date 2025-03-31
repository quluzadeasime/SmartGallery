using Smart.Business.DTOs.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Business.DTOs.TransactionDTOs
{
    public class OrderDTO : BaseEntityDTO
    {
        public decimal Amount { get; set; }
        public bool IsIncreased { get; set; }
        public DateTime PaymentOn { get; set; }
        public string Description { get; set; }
    }
}
