using AutoMapper;
using Smart.Business.DTOs.ServiceDTOs;
using Smart.Business.DTOs.SpecificationDTOs;
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
    public class SpecificationService : ISpecificationService
    {
        private readonly ISpecificationRepository _specificationRepository;
        private readonly ISpecificationHandler _specificationHandler;
        private readonly IMapper _mapper;

        public SpecificationService(ISpecificationRepository specificationRepository, IMapper mapper, ISpecificationHandler specificationHandler)
        {
            _specificationRepository = specificationRepository;
            _mapper = mapper;
            _specificationHandler = specificationHandler;
        }

        public async Task<SpecificationDTO> CreateAsync(CreateSpecificationDTO dto)
        {
            var entity = _mapper.Map<Specification>(dto);
            var result = await _specificationRepository.AddAsync(entity);

            return new SpecificationDTO
            {
                Id = result.Id,
                Key = result.Key,
                Value = result.Value
            };
        }

        public async Task<SpecificationDTO> DeleteAsync(DeleteSpecificationDTO dto)
        {
            var result = await _specificationRepository.DeleteAsync(
               _specificationHandler.HandleEntityAsync(await _specificationRepository.GetByIdAsync(x => x.Id == dto.Id)));

            return new SpecificationDTO
            {
                Id = result.Id,
                Key = result.Key,
                Value = result.Value
            };
        }

        public async Task<IEnumerable<SpecificationDTO>> GetAllAsync()
        {
            var query = await _specificationRepository.GetAllAsync(x => !x.IsDeleted);

            return query.Select(x => new SpecificationDTO
            {
                Id = x.Id,
                Key = x.Key,
                Value = x.Value
            });
        }

        public async Task<SpecificationDTO> GetByIdAsync(GetByIdSpecificationDTO dto)
        {
            var entity = _specificationHandler.HandleEntityAsync(
                await _specificationRepository.GetByIdAsync(x => x.Id == dto.Id));

            return new SpecificationDTO
            {
                Id = entity.Id,
                Key = entity.Key,
                Value = entity.Value
            };
        }

        public async Task<SpecificationDTO> UpdateAsync(UpdateSpecificationDTO dto)
        {
            var oldEntity = _specificationHandler.HandleEntityAsync(await _specificationRepository.GetByIdAsync(x => x.Id == dto.Id));
            var entity = await _specificationRepository.UpdateAsync(
                _mapper.Map(dto, oldEntity));

            return new SpecificationDTO
            {
                Id = entity.Id,
                Key = entity.Key,
                Value = entity.Value
            };
        }
    }
}
