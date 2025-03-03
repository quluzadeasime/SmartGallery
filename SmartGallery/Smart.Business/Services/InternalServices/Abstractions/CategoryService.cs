using AutoMapper;
using Smart.Business.DTOs.CategoryDTOs;
using Smart.Business.Services.InternalServices.Interfaces;
using Smart.Core.Entities;
using Smart.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Business.Services.InternalServices.Abstractions
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper = null)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CategoryDTO> CreateAsync(CreateCategoryDTO dto)
        {
            var result = await _categoryRepository.AddAsync(_mapper.Map<Category>(dto));

            return new CategoryDTO
            {
                Id = result.Id,
                Name = result.Name
            };
        }

        public async Task<CategoryDTO> DeleteAsync(DeleteCategoryDTO dto)
        {
            var result = await _categoryRepository.DeleteAsync(
                await _categoryRepository.GetByIdAsync(x => x.Id == dto.Id));

            return new CategoryDTO
            {
                Id = result.Id,
                Name = result.Name
            };
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllAsync()
        {
            var query = await _categoryRepository.GetAllAsync(x => !x.IsDeleted);

            return query.Select(x => new CategoryDTO
            {
                Id = x.Id,
                Name = x.Name
            });
        }

        public async Task<CategoryDTO> GetByIdAsync(GetByIdCategoryDTO dto)
        {
            var entity = await _categoryRepository.GetByIdAsync(x => x.Id == dto.Id);

            return new CategoryDTO
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        public async Task<CategoryDTO> UpdateAsync(UpdateCategoryDTO dto)
        {
            var oldEntity = await _categoryRepository.GetByIdAsync(x => x.Id == dto.Id);
            var result = await _categoryRepository.UpdateAsync(
                _mapper.Map(dto, oldEntity));

            return new CategoryDTO
            {
                Id = result.Id,
                Name = result.Name
            };
        }
    }
}
