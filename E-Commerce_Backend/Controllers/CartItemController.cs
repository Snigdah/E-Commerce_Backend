using E_Commerce_Backend.Dto;
using E_Commerce_Backend.Interfaces;
using E_Commerce_Backend.Models.CartModel;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartItemController : Controller
    {
        private readonly ICartItemRepository _cartItemRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;

        public CartItemController(ICartItemRepository cartItemRepository,
                                  ICartRepository cartRepository,
                                  IProductRepository productRepository)
        {
            _cartItemRepository = cartItemRepository;
            _cartRepository = cartRepository;
            _productRepository = productRepository;
        }

        // GET: api/allCartItem/{cartId}
        [HttpGet("cart/{cartId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(CartItem))]
        public async Task<IActionResult> GetAllCartItemByCartId(int cartId)
        {
            try
            {
                var cartItem = await _cartItemRepository.GetAllCartItemByCartId(cartId);

                if(cartItem == null)
                {
                    return NotFound("No cart found");
                }

                return Ok(cartItem);

            }
            catch (Exception)
            {

                return StatusCode(500, "An error occurred while retrieving the cart item.");
            }
        }

        // GET: api/allCartItem/{userId}
        [HttpGet("user/{userId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(CartItem))]
        public async Task<IActionResult> GetAllCartItemByUserId(int userId)
        {
            try
            {
                // Fetch Cart by User ID
                var cart = await _cartRepository.GetCartByUserId(userId);

                if( cart == null)
                {
                    return NotFound("This user id not exists");
                }
                
                //Fetch CartItem by Cart ID
                var cartItem = await _cartItemRepository.GetAllCartItemByCartId(cart.CartId);

                if (cartItem == null)
                {
                    return NotFound("No cart found");
                }

                return Ok(cartItem);

            }
            catch (Exception)
            {

                return StatusCode(500, "An error occurred while retrieving the cart item.");
            }
        }

        //GET: api/cartItem/{id}
        [HttpGet("{cartItemId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(CartItem))]
        public async Task<IActionResult> GetCartItemByCardId(int cartItemId)
        {
            try
            {
                //Check Cart Item Id
                var cartItem = await _cartItemRepository.GetCartItemByCardItemId(cartItemId);

                if (cartItem == null)
                {
                    return NotFound("Cart item id not exists");
                }

                return Ok(cartItem);
            }
            catch (Exception)
            {

                return StatusCode(500, "An error occurred while retrieving the Cart item.");
            }
        }

        //Post: api/cartItem
        [HttpPost]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(CartItem))]
        public async Task<IActionResult> AddCartItem(int userId, int productId, CartItemDto cartItemDto)
        {
            try
            {
                // Fetch Cart by User ID
                var cart = await _cartRepository.GetCartByUserId(userId);

                if (cart == null)
                {
                    return NotFound("This user id not exists");
                }

                // Fetch Product by ProductId
                var product = await _productRepository.GetProductById(productId);

                if (product == null)
                {
                    return NotFound("Product not found");
                }

                var cartItem = new CartItem();
                cartItem.CartItemId = cartItemDto.CartItemId;
                cartItem.Quantity = cartItemDto.Quantity;
                cartItem.ProductId = productId;
                cartItem.CartId = cart.CartId;
                cartItem.Product = product;
                cartItem.Cart = cart;
                cartItem.productName = product.Name;

                await _cartItemRepository.AddCartItem(cartItem);
                return Ok(cartItem.Product.Name + " Added successfully");
            }
            catch (Exception)
            {

                return StatusCode(500, "An error occurred while adding the cart item.");
            }
        }

        //Update Cart Item
        [HttpPut("{cartItemId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateCartItem(int cartItemId, [FromBody] CartItemDto cartItemDto)
        {
            try
            {
                //Check Cart Item
                var cartItem = await _cartItemRepository.GetCartItemByCardItemId(cartItemId);

                if(cartItem == null)
                {
                    return BadRequest("CartItem Id not exists");
                }

                cartItem.CartItemId = cartItemId;
                cartItem.Quantity = cartItemDto.Quantity;
                cartItem.ProductId = cartItem.ProductId;
                cartItem.Product = cartItem.Product;
                cartItem.productName = cartItem.productName;
                cartItem.CartId = cartItem.CartId;
                cartItem.Cart = cartItem.Cart;

                await _cartItemRepository.UpdateTaskItem(cartItem);
                return Ok("Cart Item Update Successfully");
            }
            catch (Exception)
            {

                return StatusCode(500, "An error occurred while Updating the Cart Item.");
            }
        }

        // DELETE: api/cartItem/{id}
        [HttpDelete("{cartItemId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteCartItem(int cartItemId)
        {
            try
            {   
                //Check Cart Item
                var cartItem = await _cartItemRepository.GetCartItemByCardItemId(cartItemId);

                if(cartItem == null)
                {
                    return BadRequest("Cart Item not found");
                }

                await _cartItemRepository.DeleteCartItem(cartItemId);
                return Ok("Cart Item Delete Successfully");
            }
            catch (Exception)
            {

                return StatusCode(500, "An error occurred while deleting the cart item.");
            }
        }

    }
}
