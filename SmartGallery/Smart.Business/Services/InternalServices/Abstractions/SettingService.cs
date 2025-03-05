using Smart.Business.DTOs.SettingDTOs;
using Smart.Business.Services.InternalServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Business.Services.InternalServices.Abstractions
{
    public class SettingService : ISettingService
    {
        public Task<SettingDTO> CreateAsync(CreateSettingDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<SettingDTO> DeleteAsync(DeleteSettingDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SettingDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<SettingDTO> GetByIdAsync(GetByIdSettingDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<SettingDTO> UpdateAsync(UpdateSettingDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
