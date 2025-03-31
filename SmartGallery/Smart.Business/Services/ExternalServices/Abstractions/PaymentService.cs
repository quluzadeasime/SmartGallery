using App.DAL.Presistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Smart.Business.DTOs.TransactionDTOs;
using Smart.Business.Helpers;
using Smart.Business.Services.ExternalServices.Interfaces;
using Smart.Business.Services.InternalServices.Interfaces;
using Smart.Core.Entities;
using Smart.Core.Entities.Identity;
using Smart.Core.Enums;
using Smart.DAL.Handlers.Interfaces;
using Smart.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Smart.Business.Services.ExternalServices.Abstractions
{
    public class PaymentService : IPaymentService
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly ITransactionHandler _transactionHandler;
        private readonly IAccountService _accountService;
        private readonly UserManager<User> _userManager;
        private readonly IEmailService _emailService;
        private readonly AppDbContext _appDbContext;
        private readonly IHttpContextAccessor _http;
        private readonly IConfiguration _config;
        private readonly BankClient _client;

        public PaymentService(ISubscriptionRepository subscriptionRepository, ITransactionRepository transactionRepository,
            ITransactionHandler transactionHandler, IAccountService accountService,
            UserManager<User> userManager, IEmailService emailService,
            AppDbContext appDbContext, IHttpContextAccessor http,
            IConfiguration config, BankClient client)
        {
            _subscriptionRepository = subscriptionRepository;
            _transactionRepository = transactionRepository;
            _transactionHandler = transactionHandler;
            _accountService = accountService;
            _userManager = userManager;
            _emailService = emailService;
            _appDbContext = appDbContext;
            _http = http;
            _config = config;
            _client = client;
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

        public async Task<IQueryable<OrderDTO>> GetAllAsync(GetAllOrderDTO dto)
        {
            var query = await _transactionRepository.GetAllAsync(x => !x.IsDeleted && x.UserId == dto.UserId);

            return query.Where(x => x.Status == EOrderStatus.FULLYPAID)
                .OrderByDescending(t => t.UpdatedOn)
                .Take(8)
                .Select(t => new OrderDTO
                {
                    Id = t.Id,
                    PaymentOn = t.UpdatedOn.ToLocalTime(),
                    Amount = t.Amount
                }).AsQueryable();
        }

        public async Task<string> GetOrderInformationAsync(string orderId)
        {
            var response = await _client.GetAsync($"https://txpgtst.kapitalbank.az/api/order/{orderId}");

            return response;
        }

        public async Task<TransactionDTO> IncreaseBalanceAsync(IncreaseBalanceDTO dto)
        {
            var checkToken = GeneratePaymentCheckToken();
            var orderDescription = "IncreaseBalanceOrderDescription";
            decimal netPrice = dto.Amount;

            var response = await IncreaseBalanceOrderAsync(checkToken,
                netPrice, orderDescription);

            var responseBody = await response.Content.ReadAsStringAsync();
            var responseJson = JObject.Parse(responseBody);

            var orderIdFromResponse = responseJson["order"]?["id"]?.ToString();
            var sessionIdFromResponse = responseJson["order"]?["password"]?.ToString();

            var newTransaction = new Transaction()
            {
                UserId = dto.UserId,
                OrderId = long.Parse(orderIdFromResponse),
                Amount = netPrice,
                SessionId = sessionIdFromResponse,
                CheckToken = checkToken,
                Status = EOrderStatus.ONPAYMENT,
                ResponseBody = responseBody
            };

            var createdTransaction = await _transactionRepository.AddAsync(newTransaction);

            return new TransactionDTO
            {
                Url = $"{responseJson["order"]?["hppUrl"]?.ToString()}?id={orderIdFromResponse}&password={sessionIdFromResponse}",
                Token = checkToken
            };
        }

        public Task<OrderResponseDTO> PaymentHandlerAsync(string checkToken, bool increaseOrBuy)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionDTO> PurchaseAsync(CreateTransactionDTO dto)
        {
            throw new NotImplementedException();
        }

        public string GeneratePaymentCheckToken()
        {
            byte[] numberBytes = BitConverter.GetBytes(DateTime.Now.Ticks);

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(numberBytes);

                StringBuilder hashString = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    hashString.Append(b.ToString("x2"));
                }

                return hashString.ToString();
            }
        }

        public async Task<HttpResponseMessage> IncreaseBalanceOrderAsync(
            string checkToken, decimal amount,
            string description)
        {
            var frontUrl = _config["FrontEndUrl"];

            var order = new
            {
                order = new
                {
                    typeRid = "Order_SMS",
                    amount = amount.ToString("F2"),
                    currency = "AZN",
                    description = description,
                    hppRedirectUrl = $"{frontUrl}/dashboard/increase-balance",
                    hppCofCapturePurposes = new[] { "Cit" }
                }
            };

            var json = JsonSerializer.Serialize(order, new JsonSerializerOptions { WriteIndented = true });
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("order/", content);

            return response;
        }
    }
}
