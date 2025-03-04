using Smart.Business.DTOs.BrandDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Business.Services.InternalServices.Interfaces
{
    public interface IBrandService
    {
        Task<IEnumerable<BrandDTO>> GetAllAsync();
        Task<BrandDTO> GetByIdAsync(GetByIdBrandDTO dto);
        Task<BrandDTO> CreateAsync(CreateBrandDTO dto);
        Task<BrandDTO> DeleteAsync(DeleteBrandDTO dto);
        Task<BrandDTO> UpdateAsync(UpdateBrandDTO dto);
    }
}
