using AutoMapper;
using MySqlX.XDevAPI.Common;
using Smart.Business.DTOs.ProductDTOs;
using Smart.Business.DTOs.SpecificationDTOs;
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
using static iText.IO.Util.IntHashtable;

namespace Smart.Business.Services.InternalServices.Abstractions
{
    public class ProductService : IProductService
    {
        private readonly IProductColorRepository _productColorRepository;
        private readonly IFileManagerService _fileManagerService;
        private readonly IProductRepository _productRepository;
        private readonly IProductHandler _productHandler;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper, IFileManagerService fileManagerService
            ,IProductHandler productHandler, IProductColorRepository productColorRepository)
        {
            _productColorRepository = productColorRepository;
            _fileManagerService = fileManagerService;
            _productRepository = productRepository;
            _productHandler = productHandler;
            _mapper = mapper;
        }

        public async Task<ProductDTO> CreateAsync(CreateProductDTO dto)
        {
            var entity = _mapper.Map<Product>(dto);

            if(dto.Discount is not null)
            {
                entity.DiscountedPrice = (decimal) (entity.Price * dto.Discount) / 100;
                entity.TotalPrice = entity.Price - entity.DiscountedPrice;
            }

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

            foreach (var id in dto.ColorIds)
            {
                await _productColorRepository.AddAsync(new ProductColor
                {
                    ColorId = id,
                    ProductId = result.Id
                });
            }

            return new ProductDTO
            {
                Id = result.Id,
                Name = result.Name,
                Price = result.Price,
                Rating = result.Rating,
                BrandId = result.BrandId,
                Discount = result.Discount,
                CategoryId = result.CategoryId,
                RatingCount = result.RatingCount,
                Description = result.Description,
                ProductImageUrls = entity.ProductImages.Select(x => x.ImageUrl).ToList(),
            };

        }

        public async Task<ProductDTO> DeleteAsync(DeleteProductDTO dto)
        {
            var product = await _productRepository.GetByIdAsync(x => x.Id == dto.Id);

            if (product == null)
                throw new Exception("Product not found.");

            var result = _productHandler.HandleEntityAsync(await _productRepository.DeleteAsync(product));

            return new ProductDTO
            {
                Id = result.Id,
                Name = result.Name,
                Price = result.Price,
                Rating = result.Rating,
                BrandId = result.BrandId,
                Discount = result.Discount,
                CategoryId = result.CategoryId,
                Description = result.Description,
                RatingCount = result.RatingCount,
            };
        }

        public async Task<IEnumerable<ProductDTO>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync(x => !x.IsDeleted,
                pi => pi.ProductImages,
                s => s.Specifications);

            return products.Select(x => new ProductDTO
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                Rating = x.Rating,
                BrandId = x.BrandId,
                Discount = x.Discount,
                CategoryId = x.CategoryId,
                Description = x.Description,
                RatingCount = x.RatingCount,
                ProductImageUrls = x.ProductImages.Select(x => x.ImageUrl).ToList(),
            });
        }

        public async Task<ProductDTO> GetByIdAsync(GetByIdProductDTO dto)
        {
            var product = _productHandler.HandleEntityAsync(
                await _productRepository.GetByIdAsync(x => x.Id == dto.Id, 
                pi => pi.ProductImages,
                s => s.Specifications));

            if (product == null)
                throw new Exception("Product not found.");

            return new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Rating = product.Rating,
                BrandId = product.BrandId,
                Discount = product.Discount,
                CategoryId = product.CategoryId,
                RatingCount = product.RatingCount,
                Description = product.Description,
                ProductImageUrls = product.ProductImages.Select(x => x.ImageUrl).ToList(),
                Specifications = product.Specifications.Select(x => new SpecificationDTO
                {
                    Id = x.Id,
                    Key = x.Key,
                    Value = x.Value,
                }).ToList(),
            };
        }

        public async Task<ProductDTO> UpdateAsync(UpdateProductDTO dto)
        {
            var oldProduct = _productHandler.HandleEntityAsync(
                await _productRepository.GetByIdAsync(x => x.Id == dto.Id, pi => pi.ProductImages));

            if (oldProduct == null)
                throw new Exception("Product not found.");

            var updatedProduct = await _productRepository.UpdateAsync(_mapper.Map(dto, oldProduct));

            return new ProductDTO
            {
                Id = updatedProduct.Id,
                Name = updatedProduct.Name,
                Price = updatedProduct.Price,
                Rating = updatedProduct.Rating,
                BrandId = updatedProduct.BrandId,
                Discount = updatedProduct.Discount,
                CategoryId = updatedProduct.CategoryId,
                RatingCount = updatedProduct.RatingCount,
                Description = updatedProduct.Description,
                ProductImageUrls = updatedProduct.ProductImages.Select(x => x.ImageUrl).ToList(),
            };
        }
    }
}
