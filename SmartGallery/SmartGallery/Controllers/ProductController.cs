using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Smart.API.Controllers.Commons;
using Smart.Business.DTOs.ProductDTOs;
using Smart.Business.Services.InternalServices.Abstractions;
using Smart.Business.Services.InternalServices.Interfaces;
using Smart.Business.Validators.ProductValidators;

namespace Smart.API.Controllers
{
    public class ProductController : BaseAPIController
    {
        protected readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _productService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var dto = new GetByIdProductDTO { Id = id };
            var validation = await new GetByIdProductDTOValidator().ValidateAsync(dto);

            return validation.IsValid ? Ok(await _productService.GetByIdAsync(dto))
                : BadRequest(validation.Errors.Select(x => x.ErrorMessage).ToList());
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] CreateProductDTO dto)
        {
            var validation = await new CreateProductDTOValidator().ValidateAsync(dto);

            return validation.IsValid ? Ok(await _productService.CreateAsync(dto))
                : BadRequest(validation.Errors.Select(x => x.ErrorMessage).ToList());
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromForm] UpdateProductDTO dto)
        {
            var validation = await new UpdateProductDTOValidator().ValidateAsync(dto);

            return validation.IsValid ? Ok(await _productService.UpdateAsync(dto))
                : BadRequest(validation.Errors.Select(x => x.ErrorMessage).ToList());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var dto = new DeleteProductDTO() { Id = id };
            var validation = await new DeleteProductDTOValidator().ValidateAsync(dto);

            return validation.IsValid ? Ok(await _productService.DeleteAsync(dto))
                : BadRequest(validation.Errors.Select(x => x.ErrorMessage).ToList());
        }
    }
}
