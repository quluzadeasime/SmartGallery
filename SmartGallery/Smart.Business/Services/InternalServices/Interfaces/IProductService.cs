using Smart.Business.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Business.Services.InternalServices.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetAllAsync();
        Task<ProductDTO> GetByIdAsync(GetByIdProductDTO dto);
        Task<ProductDTO> CreateAsync(CreateProductDTO dto);
        Task<ProductDTO> UpdateAsync(UpdateProductDTO dto);
        Task<ProductDTO> DeleteAsync(DeleteProductDTO dto);
    }
}
