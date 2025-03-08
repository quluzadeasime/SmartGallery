using Smart.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.DAL.Repositories.Interfaces
{
    public interface ISubscriptionRepository : IRepository<Subscription> 
    {
        Task<bool> IsSubscribedAsync(string email);
    }
}
