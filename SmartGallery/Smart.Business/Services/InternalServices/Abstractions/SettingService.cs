using AutoMapper;
using Microsoft.EntityFrameworkCore.Query.Internal;
using MySqlX.XDevAPI.Common;
using Smart.Business.DTOs.SettingDTOs;
using Smart.Business.Services.InternalServices.Interfaces;
using Smart.Core.Entities;
using Smart.DAL.Handlers.Interfaces;
using Smart.DAL.Repositories.Implementations;
using Smart.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Business.Services.InternalServices.Abstractions
{
    public class SettingService : ISettingService
    {
        private readonly ISettingRepository _settingRepository;
        private readonly ISettingHandler _settingHandler;
        private readonly IMapper _mapper;

        public SettingService(ISettingRepository settingRepository, IMapper mapper = null, ISettingHandler settingHandler = null)
        {
            _settingRepository = settingRepository;
            _mapper = mapper;
            _settingHandler = settingHandler;
        }

        public async Task<SettingDTO> CreateAsync(CreateSettingDTO dto)
        {
            var result = await _settingRepository.AddAsync(_mapper.Map<Setting>(dto));

            return new SettingDTO
            {
                Id = result.Id,
                Instagram = result.Instagram,
                Facebook = result.Facebook,
                Address = result.Address,
                Phone = result.Phone,
                WorkHours = result.WorkHours,
                Email = result.Email,
                LogoUrl = result.LogoUrl
            };
        }

        public async Task<SettingDTO> DeleteAsync(DeleteSettingDTO dto)
        {
            var result = await _settingRepository.DeleteAsync(
               _settingHandler.HandleEntityAsync(await _settingRepository.GetByIdAsync(x => x.Id == dto.Id)));

            return new SettingDTO
            {
                Id = result.Id,
                Instagram = result.Instagram,
                Facebook = result.Facebook,
                Address = result.Address,
                Phone = result.Phone,
                WorkHours = result.WorkHours,
                Email = result.Email,
                LogoUrl = result.LogoUrl
            };
        }

        public async Task<IEnumerable<SettingDTO>> GetAllAsync()
        {
            var query = await _settingRepository.GetAllAsync(x => !x.IsDeleted);

            return query.Select(x => new SettingDTO
            {
                Id = x.Id,
                Instagram = x.Instagram,
                Facebook = x.Facebook,
                Address = x.Address,
                Phone = x.Phone,
                WorkHours = x.WorkHours,
                Email = x.Email,
                LogoUrl = x.LogoUrl
            });
        }

        public async Task<SettingDTO> GetByIdAsync(GetByIdSettingDTO dto)
        {
            var entity = _settingHandler.HandleEntityAsync(
                await _settingRepository.GetByIdAsync(x => x.Id == dto.Id));

            return new SettingDTO
            {
                Id = entity.Id,
                Instagram = entity.Instagram,
                Facebook = entity.Facebook,
                Address = entity.Address,
                Phone = entity.Phone,
                WorkHours = entity.WorkHours,
                Email = entity.Email,
                LogoUrl = entity.LogoUrl
            };
        }

        public async Task<SettingDTO> UpdateAsync(UpdateSettingDTO dto)
        {
            var oldEntity = _settingHandler.HandleEntityAsync(
                await _settingRepository.GetByIdAsync(x => x.Id == dto.Id));

            var entity = await _settingRepository.UpdateAsync(
                _mapper.Map(dto, oldEntity));

            return new SettingDTO
            {
                Id = entity.Id,
                Instagram = entity.Instagram,
                Facebook = entity.Facebook,
                Address = entity.Address,
                Phone = entity.Phone,
                WorkHours = entity.WorkHours,
                Email = entity.Email,
                LogoUrl = entity.LogoUrl
            };
        }
    }
}
