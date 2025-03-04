using Smart.Business.DTOs.ServiceDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Business.Services.InternalServices.Interfaces
{
    public interface IServiceService
    {
        Task<IEnumerable<ServiceDTO>> GetAllAsync();
        Task<ServiceDTO> GetByIdAsync(GetByIdServiceDTO dto);
        Task<ServiceDTO> CreateAsync(CreateServiceDTO dto);
        Task<ServiceDTO> UpdateAsync(UpdateServiceDTO dto);
        Task<ServiceDTO> DeleteAsync(DeleteServiceDTO dto);
    }
}
