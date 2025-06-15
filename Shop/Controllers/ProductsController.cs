using Application.ProductDto;
using Application.Use_Case;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductCase _productUseCase;

       public ProductsController(ProductCase productUseCase)
        {
            _productUseCase = productUseCase;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<ProductDtos>>> GetAll()
        {
             IEnumerable<ProductDtos> products = await _productUseCase.getAllProducts();

            return Ok(products);

        }

        [HttpGet ("{idProduct:Guid},")]

        public async Task<ActionResult<ProductDtos>> GetById(Guid id)
        {
           
           var product = await _productUseCase.getById(id);

            return Ok(product);

        }

        [HttpGet ( "{idCategory:Guid}")]

        public async Task <ActionResult<List<ProductDtos>>> getByCategory(Guid categoryId)
        {
            var products = await _productUseCase.getProductsByCategory(categoryId);

            return Ok(products);
        }

        [HttpPost]

        public async Task<ActionResult<ProductDtos>> Create(CreateProductDto model)
        {
             var createdProduct  =    await _productUseCase.CreateProductAsync(model);

            return Ok(createdProduct);
        }

        [HttpPut("{IdUpdate:Guid}")]

        public async Task<ActionResult<ProductDtos>> UpdateProduct(Guid id , UpdateProductDto UpProduct)
        {
            var product = await _productUseCase.UpdateProduct(id, UpProduct);

            return Ok(product);
        }


        
        
    }
}
