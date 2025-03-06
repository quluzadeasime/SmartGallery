using AutoMapper;
using Smart.Business.DTOs.ProductDTOs;
using Smart.Business.DTOs.SpecificationDTOs;
using Smart.Business.Services.ExternalServices.Interfaces;
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
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IFileManagerService _fileManagerService;

        public ProductService(IProductRepository productRepository, IMapper mapper = null, IFileManagerService fileManagerService = null)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _fileManagerService = fileManagerService;
        }

        public async Task<ProductDTO> CreateAsync(CreateProductDTO dto)
        {
            var entity = _mapper.Map<Product>(dto);

            
            var uploadedImageUrls = new List<string>();
            if (dto.Images != null && dto.Images.Any())
            {
                foreach (var image in dto.Images)
                {
                    var imageUrl = await _fileManagerService.UploadFileAsync(image);
                    uploadedImageUrls.Add(imageUrl);
                }
            }

            
            entity.ProductImages = uploadedImageUrls.Select(url => new ProductImage { ImageUrl = url }).ToList();

            var result = await _productRepository.AddAsync(entity);

            return new ProductDTO
            {
                Id = result.Id,
                Name = result.Name,
                Price = result.Price,
                Discount = result.Discount,
                Rating = result.Rating,
                RatingCount = result.RatingCount,
                Description = result.Description,
                CategoryId = result.CategoryId,
                BrandId = result.BrandId,
                ProductImages = result.ProductImages,
                //Specifications = result.Specifications
                Specifications = result.Specifications.Select(spec => new SpecificationDTO
                {
                    Key = spec.Key,
                    Value = spec.Value
                }).ToList()
            };
        }


        public async Task<ProductDTO> DeleteAsync(DeleteProductDTO dto)
        {
            var product = await _productRepository.GetByIdAsync(x => x.Id == dto.Id);

            if (product == null)
                throw new Exception("Product not found.");

            var result = await _productRepository.DeleteAsync(product);

            return new ProductDTO
            {
                Id = result.Id,
                Name = result.Name,
                Price = result.Price,
                Discount = result.Discount,
                Rating = result.Rating,
                RatingCount = result.RatingCount,
                Description = result.Description,
                CategoryId = result.CategoryId,
                BrandId = result.BrandId,
                ProductImages = result.ProductImages
            };
        }

        public async Task<IEnumerable<ProductDTO>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync(x => !x.IsDeleted);

            return products.Select(x => new ProductDTO
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                Discount = x.Discount,
                Rating = x.Rating,
                RatingCount = x.RatingCount,
                Description = x.Description,
                CategoryId = x.CategoryId,
                BrandId = x.BrandId,
                ProductImages = x.ProductImages
            });
        }

        public async Task<ProductDTO> GetByIdAsync(GetByIdProductDTO dto)
        {
            var product = await _productRepository.GetByIdAsync(x => x.Id == dto.Id);

            if (product == null)
                throw new Exception("Product not found.");

            return new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Discount = product.Discount,
                Rating = product.Rating,
                RatingCount = product.RatingCount,
                Description = product.Description,
                CategoryId = product.CategoryId,
                BrandId = product.BrandId,
                ProductImages = product.ProductImages
            };
        }

        public async Task<ProductDTO> UpdateAsync(UpdateProductDTO dto)
        {
            var oldProduct = await _productRepository.GetByIdAsync(x => x.Id == dto.Id);

            if (oldProduct == null)
                throw new Exception("Product not found.");

            var updatedProduct = await _productRepository.UpdateAsync(_mapper.Map(dto, oldProduct));

            return new ProductDTO
            {
                Id = updatedProduct.Id,
                Name = updatedProduct.Name,
                Price = updatedProduct.Price,
                Discount = updatedProduct.Discount,
                Rating = updatedProduct.Rating,
                RatingCount = updatedProduct.RatingCount,
                Description = updatedProduct.Description,
                CategoryId = updatedProduct.CategoryId,
                BrandId = updatedProduct.BrandId,
                ProductImages = updatedProduct.ProductImages
            };
        }
    }
}
