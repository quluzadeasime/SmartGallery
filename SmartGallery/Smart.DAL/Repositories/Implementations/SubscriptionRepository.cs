using App.DAL.Presistence;
using Microsoft.EntityFrameworkCore;
using Smart.Core.Entities;
using Smart.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.DAL.Repositories.Implementations
{
    public class SubscriptionRepository : Repository<Subscription>, ISubscriptionRepository
    {
        private readonly AppDbContext _context;
        public SubscriptionRepository(AppDbContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<bool> IsSubscribedAsync(string email)
        {
            return await _context.Subscriptions.AnyAsync(x => x.Email == email && !x.IsDeleted);
        }
    }
}
