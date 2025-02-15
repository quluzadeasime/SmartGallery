using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Shared.Interfaces
{
    public interface IClaimService
    {
        string GetUserId();
        string GetClaim(string key);
    }
}
