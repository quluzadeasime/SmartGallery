using Smart.Business.DTOs.SettingDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Business.Services.InternalServices.Interfaces
{
    public interface ISettingService
    {
        Task<IEnumerable<SettingDTO>> GetAllAsync();
        Task<SettingDTO> GetByIdAsync(GetByIdSettingDTO dto);
        Task<SettingDTO> CreateAsync(CreateSettingDTO dto);
        Task<SettingDTO> UpdateAsync(UpdateSettingDTO dto);
        Task<SettingDTO> DeleteAsync(DeleteSettingDTO dto);
    }
}
