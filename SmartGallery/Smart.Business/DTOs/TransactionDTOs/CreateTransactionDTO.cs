using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Business.DTOs.TransactionDTOs
{
    public class CreateTransactionDTO
    {
        public bool Type { get; set; }
        public int? SubId { get; set; }
        public string UserId { get; set; }
        public string? PromotionCode { get; set; }
        public int? InstallmentNumber { get; set; }
    }
}
