using Smart.Business.DTOs.SpecificationDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Business.Services.InternalServices.Interfaces
{
    public interface ISpecificationService
    {
        Task<IEnumerable<SpecificationDTO>> GetAllAsync();
        Task<SpecificationDTO> GetByIdAsync(GetByIdSpecificationDTO dto);
        Task<SpecificationDTO> CreateAsync(CreateSpecificationDTO dto);
        Task<SpecificationDTO> DeleteAsync(DeleteSpecificationDTO dto);
        Task<SpecificationDTO> UpdateAsync(UpdateSpecificationDTO dto);
    }
}
