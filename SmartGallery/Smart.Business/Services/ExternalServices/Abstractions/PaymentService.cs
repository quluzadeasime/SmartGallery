using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Smart.Business.Helpers;
using Smart.Business.Services.ExternalServices.Interfaces;

namespace Smart.Business.Services.ExternalServices.Abstractions
{
    public class PaymentService : IPaymentService
    {
        private readonly BankClient _client;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _http;

        public PaymentService(
            BankClient client, IConfiguration config, 
            IHttpContextAccessor http)
        {
            _client = client;
            _config = config;
            _http = http;
        }

        public async Task<HttpResponseMessage> CreateOrderAsync(string checkToken, decimal amount,
            string description)
        {
            var order = new
            {
                order = new
                {
                    typeRid = "Order_SMS",
                    amount = amount.ToString("F2"),
                    currency = "AZN",
                    language = "en",
                    description = description,
                    hppRedirectUrl = $"{"localhost"}/success",
                    hppCofCapturePurposes = new[] { "Cit" }
                }
            };

            var json = JsonSerializer.Serialize(order, new JsonSerializerOptions { WriteIndented = true });
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("order/", content);

            return response;
        }

        public async Task<string> GetOrderInformationAsync(string orderId)
        {
            var response = await _client.GetAsync($"https://txpgtst.kapitalbank.az/api/order/{orderId}");

            return response;
        }
    }
}
