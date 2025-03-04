using AutoMapper;
using MySqlX.XDevAPI.Common;
using Smart.Business.DTOs.CategoryDTOs;
using Smart.Business.DTOs.ColorDTOs;
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
    public class ColorService : IColorService
    {
        private readonly IColorRepository _colorRepository;
        private readonly IMapper _mapper;
        public ColorService(IColorRepository colorRepository, IMapper mapper = null)
        {
            _colorRepository = colorRepository;
            _mapper = mapper;
        }

        public async Task<ColorDTO> CreateAsync(CreateColorDTO dto)
        {
            var result = await _colorRepository.AddAsync(_mapper.Map<Color>(dto));

            return new ColorDTO
            {
                Id = result.Id,
                Name = result.Name
            };
        }

        public async Task<ColorDTO> DeleteAsync(DeleteColorDTO dto)
        {
            var result = await _colorRepository.DeleteAsync(
                await _colorRepository.GetByIdAsync(x => x.Id == dto.Id));

            return new ColorDTO
            {
                Id = result.Id,
                Name = result.Name
            };
        }

        public async Task<IEnumerable<ColorDTO>> GetAllAsync()
        {
            var query = await _colorRepository.GetAllAsync(x => !x.IsDeleted);

            return query.Select(x => new ColorDTO
            {
                Id = x.Id,
                Name = x.Name
            });
        }

        public async Task<ColorDTO> GetByIdAsync(GetByIdColorDTO dto)
        {
            var entity = await _colorRepository.GetByIdAsync(x => x.Id == dto.Id);

            return new ColorDTO
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        public async Task<ColorDTO> UpdateAsync(UpdateColorDTO dto)
        {
            var oldEntity = await _colorRepository.GetByIdAsync(x => x.Id == dto.Id);
            var entity = await _colorRepository.UpdateAsync(
                _mapper.Map(dto, oldEntity));

            return new ColorDTO
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }
    }
}
