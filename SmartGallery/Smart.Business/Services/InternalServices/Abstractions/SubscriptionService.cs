using AutoMapper;
using Smart.Business.DTOs.SubscriptionDTOs;
using Smart.Business.DTOs.SubscriptionDTOs;
using Smart.Business.Services.InternalServices.Interfaces;
using Smart.Core.Entities;
using Smart.DAL.Handlers.Implementations;
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
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly ISubscriptionHandler _subscriptionHandler;
        private readonly IMapper _mapper;

        public SubscriptionService(ISubscriptionRepository subscriptionRepository, ISubscriptionHandler subscriptionHandler, IMapper mapper)
        {
            _subscriptionRepository = subscriptionRepository;
            _subscriptionHandler = subscriptionHandler;
            _mapper = mapper;
        }

        public async Task<SubscriptionDTO> CreateAsync(CreateSubscriptionDTO dto)
        {
            if (await _subscriptionRepository.IsSubscribedAsync(dto.Email))
            {
                throw new Exception("Siz artıq abunəsiniz.");
            }

            var result = await _subscriptionRepository.AddAsync(_mapper.Map<Subscription>(dto));

            return new SubscriptionDTO
            {
                Id = result.Id,
                Email = result.Email
            };
        }
        public async Task<SubscriptionDTO> DeleteAsync(DeleteSubscriptionDTO dto)
        {
            var entity = await _subscriptionRepository.DeleteAsync(
               _subscriptionHandler.HandleEntityAsync(await _subscriptionRepository.GetByIdAsync(x => x.Id == dto.Id)));

            return new SubscriptionDTO
            {
                Id = entity.Id,
                Email = entity.Email
            };
        }

        public async Task<IEnumerable<SubscriptionDTO>> GetAllAsync()
        {
            var query = await _subscriptionRepository.GetAllAsync(x => !x.IsDeleted);

            return query.Select(x => new SubscriptionDTO
            {
                Id = x.Id,
                Email = x.Email
            });
        }

        public async Task<SubscriptionDTO> GetByIdAsync(GetByIdSubscriptionDTO dto)
        {
            var entity = _subscriptionHandler.HandleEntityAsync(
                await _subscriptionRepository.GetByIdAsync(x => x.Id == dto.Id));

            return new SubscriptionDTO
            {
                Id = entity.Id,
                Email = entity.Email,
            };
        }

        public async Task<bool> IsSubscribedAsync(string email)
        {
            return await _subscriptionRepository.IsSubscribedAsync(email);
        }
    }
}
