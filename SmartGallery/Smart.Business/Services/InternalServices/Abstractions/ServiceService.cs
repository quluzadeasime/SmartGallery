using AutoMapper;
using MySqlX.XDevAPI.Common;
using Smart.Business.DTOs.ColorDTOs;
using Smart.Business.DTOs.ServiceDTOs;
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
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IServiceHandler _serviceHandler;
        private readonly IMapper _mapper;
        public ServiceService(IServiceRepository serviceRepository, IMapper mapper, IServiceHandler serviceHandler)
        {
            _serviceRepository = serviceRepository;
            _mapper = mapper;
            _serviceHandler = serviceHandler;
        }

        public async Task<ServiceDTO> CreateAsync(CreateServiceDTO dto)
        {
            var result = await _serviceRepository.AddAsync(_mapper.Map<Service>(dto));

            return new ServiceDTO
            {
                Id = result.Id,
                Title = result.Title,
                Description = result.Description,
                Icon = result.Icon
            };
        }

        public async Task<ServiceDTO> DeleteAsync(DeleteServiceDTO dto)
        {
            var result = await _serviceRepository.DeleteAsync(
                _serviceHandler.HandleEntityAsync(await _serviceRepository.GetByIdAsync(x => x.Id == dto.Id)));

            return new ServiceDTO
            {
                Id = result.Id,
                Title = result.Title,
                Description = result.Description,
                Icon = result.Icon
            };
        }

        public async Task<IEnumerable<ServiceDTO>> GetAllAsync()
        {
            var query = await _serviceRepository.GetAllAsync(x => !x.IsDeleted);

            return query.Select(x => new ServiceDTO
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Icon = x.Icon
            });
        }

        public async Task<ServiceDTO> GetByIdAsync(GetByIdServiceDTO dto)
        {
            var entity = _serviceHandler.HandleEntityAsync(
                await _serviceRepository.GetByIdAsync(x => x.Id == dto.Id));

            return new ServiceDTO
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                Icon = entity.Icon
            };
        }

        public async Task<ServiceDTO> UpdateAsync(UpdateServiceDTO dto)
        {
            var oldEntity = _serviceHandler.HandleEntityAsync(
                await _serviceRepository.GetByIdAsync(x => x.Id == dto.Id));

            var entity = await _serviceRepository.UpdateAsync(
                _mapper.Map(dto, oldEntity));

            return new ServiceDTO
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                Icon = entity.Icon
            };
        }
    }
}
