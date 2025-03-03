using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Smart.API.Controllers.Commons;
using Smart.Business.DTOs.CategoryDTOs;
using Smart.Business.Services.InternalServices.Interfaces;
using Smart.Business.Validators.CategoryValidators;

namespace Smart.API.Controllers
{
    public class CategoryController : BaseAPIController
    {
        protected readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _categoryService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var dto = new GetByIdCategoryDTO { Id = id };
            var validation = await new GetByIdCategoryDTOValidator().ValidateAsync(dto);

            return validation.IsValid ? Ok(await _categoryService.GetByIdAsync(dto))
                : BadRequest(validation.Errors.Select(x => x.ErrorMessage).ToList());
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] CreateCategoryDTO dto)
        {
            var validation = await new CreateCategoryDTOValidator().ValidateAsync(dto);

            return validation.IsValid ? Ok(await _categoryService.CreateAsync(dto))
                : BadRequest(validation.Errors.Select(x => x.ErrorMessage).ToList());
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromForm] UpdateCategoryDTO dto)
        {
            var validation = await new UpdateCategoryDTOValidator().ValidateAsync(dto);

            return validation.IsValid ? Ok(await _categoryService.UpdateAsync(dto))
                : BadRequest(validation.Errors.Select(x => x.ErrorMessage).ToList());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var dto = new DeleteCategoryDTO() { Id = id };
            var validation = await new DeleteCategoryDTOValidator().ValidateAsync(dto);

            return validation.IsValid ? Ok(await _categoryService.DeleteAsync(dto))
                : BadRequest(validation.Errors.Select(x => x.ErrorMessage).ToList());
        }

    }
}
