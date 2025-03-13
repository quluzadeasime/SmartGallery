using AutoMapper;
using Smart.Business.DTOs.BrandDTOs;
using Smart.Business.DTOs.CategoryDTOs;
using Smart.Business.Services.InternalServices.Interfaces;
using Smart.Core.Entities;
using Smart.DAL.Handlers.Interfaces;
using Smart.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Business.Services.InternalServices.Abstractions
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IBrandHandler _brandHandler;
        private readonly IMapper _mapper;

        public BrandService(IBrandRepository brandRepository, IMapper mapper, IBrandHandler brandHandler)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
            _brandHandler = brandHandler;
        }

        public async Task<BrandDTO> CreateAsync(CreateBrandDTO dto)
        {
            var result = await _brandRepository.AddAsync(_mapper.Map<Brand>(dto));

            return new BrandDTO
            {
                Id = result.Id,
                ImageUrl = result.ImageUrl
            };
        }

        public async Task<BrandDTO> DeleteAsync(DeleteBrandDTO dto)
        {
            var result = await _brandRepository.DeleteAsync(
                _brandHandler.HandleEntityAsync(await _brandRepository.GetByIdAsync(x => x.Id == dto.Id)));

            return new BrandDTO
            {
                Id = result.Id,
                ImageUrl = result.ImageUrl
            };
        }

        public async Task<IEnumerable<BrandDTO>> GetAllAsync()
        {
            var query = await _brandRepository.GetAllAsync(x => !x.IsDeleted);

            return query.Select(x => new BrandDTO
            {
                Id = x.Id,
                ImageUrl = x.ImageUrl
            });
        }

        public async Task<BrandDTO> GetByIdAsync(GetByIdBrandDTO dto)
        {
            var entity = _brandHandler.HandleEntityAsync(
                await _brandRepository.GetByIdAsync(x => x.Id == dto.Id));

            return new BrandDTO
            {
                Id = entity.Id,
                ImageUrl = entity.ImageUrl
            };
        }

        public async Task<BrandDTO> UpdateAsync(UpdateBrandDTO dto)
        {
            var oldEntity = _brandHandler.HandleEntityAsync(
                await _brandRepository.GetByIdAsync(x => x.Id == dto.Id));

            var result = await _brandRepository.UpdateAsync(
                _mapper.Map(dto, oldEntity));

            return new BrandDTO
            {
                Id = result.Id,
                ImageUrl = result.ImageUrl
            };
        }
    }
}
