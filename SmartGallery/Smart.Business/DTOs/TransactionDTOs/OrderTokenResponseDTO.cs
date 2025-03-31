using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Business.DTOs.TransactionDTOs
{
    public class OrderTokenResponseDTO
    {
        public string Token { get; set; }
        public bool IncreaseOrBuy { get; set; }
    }
}
