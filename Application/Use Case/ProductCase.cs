using Application.ProductDto;
using AutoMapper;
using Domain.Entities;
using Domain.IReposotory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Use_Case
{
    public class ProductCase
    {
        private readonly IProducts _productRepository;

        private readonly IMapper _mapper;
        public ProductCase(IProducts productRepository, IMapper mapper)
        {

            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDtos>> getAllProducts()
        {
            var products = await _productRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<ProductDtos>>(products);
        }

        public async Task<ProductDtos> getById(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new Exception("Incorect Id");
            }

            var product = await _productRepository.GetAsync(p => p.Id == id);

            if (product == null)
            {

                throw new Exception("Продукт не найден");
            }


            return _mapper.Map<ProductDtos>(product);
        }





        public async Task<ProductDtos> CreateProductAsync(CreateProductDto modelDto)
        {
            if (await _productRepository.GetAsync(p => p.Name == modelDto.Name) != null)
            {
                throw new Exception(" this product exist in db");
            }

            var product = _mapper.Map<Product>(modelDto);

            await _productRepository.CreateAsync(product);

            return _mapper.Map<ProductDtos>(product);
        }

        public async Task Delete(Guid id)
        {
            var product = await _productRepository.GetAsync(p => p.Id == id);

            await _productRepository.Remove(product);

            await _productRepository.SaveAsync();
        }

        public async Task<List<ProductDtos>> getProductsByCategory(Guid categoryId)
        {
            var categoryProducts = await _productRepository.GetProductsByCategoryAsync(categoryId);

            return _mapper.Map<List<ProductDtos>>(categoryProducts);
        }

        public async Task<ProductDtos> UpdateProduct(Guid productId, UpdateProductDto updateProduct)
        {
            
                var product = await _productRepository.GetAsync(p => p.Id == productId);

                if (product == null)
                    throw new Exception("Продукт не найден");

                 _mapper.Map(updateProduct, product);

              

                await _productRepository.UpdateAsync(product);

                return _mapper.Map<ProductDtos>(product);
            

        }
    }
}
