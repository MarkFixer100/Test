using Application.ProductDto;
using Application.Use_Case;
using Domain.Entities;
using Domain.IReposotory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Shop.Controllers
{
    [Route("api/ProductsApi")]
    [ApiController]
    [Produces("application/json")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductCase _productUseCase;
        private readonly ICacheService _cache;
        public ProductsController(ProductCase productUseCase , ICacheService cache)
        {
            _productUseCase = productUseCase;
            _cache = cache;
           
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ProductDtos>>> GetAllProducts()
        {
            try
            {
                var products = await _productUseCase.getAllProducts();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
            }
        }

  
        [HttpGet("paginated")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PaginatedList<Product>>> GetPaginatedProducts(
            [FromQuery, Range(0, int.MaxValue)] int pageIndex = 0,
            [FromQuery, Range(1, 100)] int pageSize = 10)
        {
            try
            {
          
      

                var products = await _productUseCase.getPaginatedSlice(pageIndex, pageSize);
                return Ok(products);
            }
            catch (ArgumentException ex)
            {
               
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
        
                return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
            }
        }


        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Product>>> SearchProducts
            (
            [FromQuery,MinLength(1)] string? nameFilter = null,
            [FromQuery] decimal? price = null
            )
        {
            try
            {
                string key = nameFilter;

                var cachedProduct = await _cache.GetAsync<string>(key);

                Console.WriteLine(cachedProduct);

                if (cachedProduct != null)
                {

                    Console.WriteLine(cachedProduct);
                    var product = JsonSerializer.Deserialize<IEnumerable<Product>>(cachedProduct);
                    return Ok(product);
                }


              

                var products = await _productUseCase.getByQuery(nameFilter, price);

                await _cache.SetAsync(key, products, TimeSpan.FromMinutes(3));

                return Ok(products);
            }
            catch (ArgumentException ex)
            {
                
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
         

                Console.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
            }
        }

        [HttpGet("Category")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProductDtos>> GetProductById([FromQuery] Guid id)
        {
            try
            {
            

                var product = await _productUseCase.getById(id);

                if (product == null)
                {
                 
                    return NotFound($"Продукт с ID {id} не найден");
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
          
                return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
            }
        }

  
        [HttpGet("category/{categoryId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<ProductDtos>>> GetProductsByCategory(Guid categoryId)
        {
            try
            {
           

                var products = await _productUseCase.getProductsByCategory(categoryId);
                return Ok(products);
            }
            catch (Exception ex)
            {
                
                return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
            }
        }

        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProductDtos>> CreateProduct([FromBody] CreateProductDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

              

                var createdProduct = await _productUseCase.CreateProductAsync(model);

                return CreatedAtAction(
                    nameof(GetProductById),
                    new { id = createdProduct.Id },
                    createdProduct);
            }
            catch (ArgumentException ex)
            {

                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
            }
        }

       
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProductDtos>> UpdateProduct(Guid id, [FromBody] UpdateProductDto updateModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

           

                var updatedProduct = await _productUseCase.UpdateProduct(id, updateModel);

                if (updatedProduct == null)
                {
                 
                    return NotFound($"Продукт с ID {id} не найден");
                }

                return Ok(updatedProduct);
            }
            catch (ArgumentException ex)
            {
             
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
           
                return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
            }
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            try
            {
             

                 await _productUseCase.Delete(id);


                return NoContent();
            }
            catch (Exception ex)
            {
             
                return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
            }
        }
    }
}