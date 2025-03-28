using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Business.Services.ExternalServices.Interfaces
{
    public interface IPaymentService
    {
        Task<HttpResponseMessage> CreateOrderAsync(string checkToken, decimal amount,
            string description);

        Task<string> GetOrderInformationAsync(string orderId);
    }
}
