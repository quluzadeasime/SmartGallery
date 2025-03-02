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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClaimService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetClaim(string key)
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirst(key)?.Value;
        }

        public string GetUserId()
        {
            return GetClaim(ClaimTypes.Name);
        }
    }
    
}
