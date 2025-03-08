using Smart.Business.DTOs.ContactDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Business.Services.InternalServices.Interfaces
{
    public interface IContactService
    {
        Task<IEnumerable<ContactDTO>> GetAllAsync();
        Task<ContactDTO> GetByIdAsync(GetByIdContactDTO dto);
        Task<ContactDTO> CreateAsync(CreateContactDTO dto);
        Task<ContactDTO> DeleteAsync(DeleteContactDTO dto);
    }
}
