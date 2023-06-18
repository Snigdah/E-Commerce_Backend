using AutoMapper;
using E_Commerce_Backend.Interfaces;
using E_Commerce_Backend.Models;
using E_Commerce_Backend.Models.ProductModel;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductController(IProductRepository productRepository, IMapper mapper )
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        // GET: api/product/
        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(Product))]
        public async Task<IActionResult> GetProduct(int pageSize, int pageNumber)
        {
            try
            {
                var product = await _productRepository.GetProducts(pageSize , pageNumber);
                if(product == null)
                {
                    return NotFound("Product no found");
                }

                return Ok(product);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while retrieving the product.");
            }
        }


        //GET: api/product/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(Product))]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                var product = await _productRepository.GetProductById(id);
                if (product == null)
                {
                    return NotFound("Product not found");
                }

                return Ok(product);
            }
            catch (Exception)
            {

                return StatusCode(500, "An error occurred while retrieving the product.");
            }
        }


        //POST: api/product
        [HttpPost]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(Product))]
        public async Task<IActionResult> AddProduct([FromBody] ProductDto productDto)
        {
            try
            {

                var product = new Product();
                product.ProductId = productDto.ProductId;
                product.Name = productDto.Name;
                product.Description = productDto.Description;
                product.Price   = productDto.Price;
                product.Reviews = null;

                await _productRepository.AddProduct(product);
                return Ok(product.Name + "Create Successfully");
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while adding the product.");
            }
        }


        //Update Product
        [HttpPut("{productId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateProduct(int productId, [FromBody] ProductDto productDto)
        {
            try
            {
                var product = await _productRepository.GetProductById(productId);

                if (product == null)
                {
                    return BadRequest("Product not fouond");
                }

                product.ProductId = productDto.ProductId;
                product.Name = productDto.Name;
                product.Description = productDto.Description;
                product.Price = productDto.Price;
                product.Reviews = product.Reviews;
               
                await _productRepository.UpdateProductAsync(product);
                return Ok("Product Update Successfully");
            }
            catch (Exception)
            {

                return StatusCode(500, "An error occurred while Updating the product.");
            }
        }


        // DELETE: api/product/{id}
        [HttpDelete("{productId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            try
            {
                var product = await _productRepository.GetProductById(productId);
                if (product == null)
                {
                    return BadRequest("Product not found");
                }

                await _productRepository.DeleteProductAsync(productId);
                return Ok("Product Delete Successfully");
            }
            catch (Exception)
            {

                return StatusCode(500, "An error occurred while deleting the product.");
            }
            
        }


    }
}
