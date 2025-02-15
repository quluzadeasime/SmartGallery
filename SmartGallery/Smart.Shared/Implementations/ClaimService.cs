using Microsoft.AspNetCore.Http;
using Smart.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Shared.Implementations
{
    public class ClaimService : IClaimService
    {
        private readonly HttpContextAccessor _httpContextAccessor;

        public ClaimService(HttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserId()
        {
            return GetClaim(ClaimTypes.Name);
        }

        public string GetClaim(string key)
        {
            var result = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value
                ?? _httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault()?.Value;

            return result;
        }
    }
}
