﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Business.DTOs.TransactionDTOs
{
    public class IncreaseBalanceDTO
    {
        public string UserId { get; set; }
        public decimal Amount { get; set; }
    }
}
