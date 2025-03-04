using Smart.Business.DTOs.ColorDTOs;
using Smart.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Business.Services.InternalServices.Interfaces
{
    public interface IColorService
    {
        Task<IEnumerable<ColorDTO>> GetAllAsync();
        Task<ColorDTO> GetByIdAsync(GetByIdColorDTO dto);
        Task<ColorDTO> CreateAsync(CreateColorDTO dto);
        Task<ColorDTO> UpdateAsync(UpdateColorDTO dto);
        Task<ColorDTO> DeleteAsync(DeleteColorDTO dto);
    }
}
