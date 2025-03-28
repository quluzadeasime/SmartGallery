using AutoMapper;
using Microsoft.AspNetCore.Http;
using Smart.Business.DTOs.BrandDTOs;
using Smart.Business.DTOs.CategoryDTOs;
using Smart.Business.Services.ExternalServices.Interfaces;
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
        private readonly IFileManagerService _fileManagerService;
        private readonly IBrandRepository _brandRepository;
        private readonly IBrandHandler _brandHandler;
        private readonly IMapper _mapper;

        public BrandService(IBrandRepository brandRepository, IMapper mapper, IBrandHandler brandHandler, IFileManagerService fileManagerService = null)
        {
            _fileManagerService = fileManagerService;
            _brandRepository = brandRepository;
            _brandHandler = brandHandler;
            _mapper = mapper;
        }

        public async Task<BrandDTO> CreateAsync(CreateBrandDTO dto)
        {
            var entity = _mapper.Map<Brand>(dto);

            if (dto.Image != null)
            {
                entity.ImageUrl = await _fileManagerService.UploadFileAsync(dto.Image);
            }

            var result = await _brandRepository.AddAsync(entity);

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
            var oldEntity = await _brandRepository.GetByIdAsync(x => x.Id == dto.Id);
            if (oldEntity == null)
            {
                throw new Exception("Brand not found.");
            }

            if (dto.Image != null)
            {
                oldEntity.ImageUrl = await _fileManagerService.UploadFileAsync(dto.Image); 
            }

            _mapper.Map(dto, oldEntity); 

            var result = await _brandRepository.UpdateAsync(oldEntity);

            return new BrandDTO
            {
                Id = result.Id,
                ImageUrl = result.ImageUrl
            };
        }

    }
}
