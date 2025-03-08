using Smart.Business.DTOs.SubscriptionDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Business.Services.InternalServices.Interfaces
{
    public interface ISubscriptionService
    {
        Task<IEnumerable<SubscriptionDTO>> GetAllAsync();
        Task<SubscriptionDTO> GetByIdAsync(GetByIdSubscriptionDTO dto);
        Task<SubscriptionDTO> CreateAsync(CreateSubscriptionDTO dto);
        Task<SubscriptionDTO> DeleteAsync(DeleteSubscriptionDTO dto);
        Task<bool> IsSubscribedAsync(string email);
    }
}
