using AutoMapper;
using Smart.Business.DTOs.ContactDTOs;
using Smart.Business.Services.InternalServices.Interfaces;
using Smart.Core.Entities;
using Smart.DAL.Handlers.Interfaces;
using Smart.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static iText.IO.Util.IntHashtable;

namespace Smart.Business.Services.InternalServices.Abstractions
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;
        private readonly IContactHandler _contactHandler;

        public ContactService(IContactRepository contactRepository, IMapper mapper = null, IContactHandler contactHandler = null)
        {
            _contactRepository = contactRepository;
            _mapper = mapper;
            _contactHandler = contactHandler;
        }

        public async Task<ContactDTO> CreateAsync(CreateContactDTO dto)
        {
            var result = await _contactRepository.AddAsync(_mapper.Map<Contact>(dto));

            return new ContactDTO
            {
                Email = result.Email,
                Name = result.Name,
                Phone = result.Phone,
                Message = result.Message
            };
        }

        public async Task<ContactDTO> DeleteAsync(DeleteContactDTO dto)
        {
            var entity = await _contactRepository.DeleteAsync(
               _contactHandler.HandleEntityAsync(await _contactRepository.GetByIdAsync(x => x.Id == dto.Id)));

            return new ContactDTO
            {
                Name = entity.Name,
                Email = entity.Email,
                Phone = entity.Phone,
                Message = entity.Message
            };
        }

        public async Task<IEnumerable<ContactDTO>> GetAllAsync()
        {
            var query = await _contactRepository.GetAllAsync(x => !x.IsDeleted);

            return query.Select(x => new ContactDTO
            {
                Name = x.Name,
                Email = x.Email,
                Phone = x.Phone,
                Message = x.Message
            });
        }

        public async Task<ContactDTO> GetByIdAsync(GetByIdContactDTO dto)
        {
            var entity = _contactHandler.HandleEntityAsync(
                await _contactRepository.GetByIdAsync(x => x.Id == dto.Id));

            return new ContactDTO
            {
                Name = entity.Name,
                Email = entity.Email,
                Phone = entity.Phone,
                Message = entity.Message
            };
        }
    }
}
