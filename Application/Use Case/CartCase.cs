using Application.CartDto;
using Application.ProductDto;
using AutoMapper;
using Domain.Entities;
using Domain.IReposotory;
using System.Text.Json;

namespace Application.Use_Case
{
    public class CartCase
    {
        private readonly ICart _cartRepository;

        private readonly IMapper _mapper;

        private readonly IProducts _productRepository;

        private readonly ICartItem _cartItemsReposirory;

        private readonly ICacheService _cache;

        public CartCase(ICart cartRepository, IMapper mapper, IProducts productRepository, ICartItem cartItemsReposirory, ICacheService cache)
        {
            _cartRepository = cartRepository;

            _mapper = mapper;
            _productRepository = productRepository;
            _cartItemsReposirory = cartItemsReposirory;
            _cache = cache;
        }

        public async Task<ResponseItemDto> addItemInCart(Guid userId, ResponseItemDto cartItem)
        {
            Cart cart = await _cartRepository.GetCartByUserId(userId);

            if(cart == null)
            {
                return null;
            }
            var productInCartitem = cart.Items.FirstOrDefault(p => p.ProductId == cartItem.ProductId);

            if (productInCartitem == null)
            {
                var product = await _productRepository.GetAsync(o => o.Id == cartItem.ProductId);

                var newCartItem = new CartItem
                {
                    Id = Guid.NewGuid(),

                    ProductId = cartItem.ProductId,

                    Product = product,

                    Cart = cart,

                    CartId = cart.Id,
                   
                    Quantity = cartItem.Quantity > 0 ? cartItem.Quantity : 1,

                };

                
             
                await _cartItemsReposirory.Add(newCartItem);
          
                return new ResponseItemDto { ProductId = newCartItem.ProductId ,CartId = newCartItem.CartId , };
            }



            productInCartitem.Quantity++;

             await _cartRepository.SaveChanges();

            return  new ResponseItemDto { ProductId = productInCartitem.ProductId, CartId = productInCartitem.CartId };

        }

        public async Task<ResponseItemDto?> removeItemInCart(Guid userId, ResponseItemDto cartItem)
        {
            var cart = await _cartRepository.GetCartByUserId(userId);

            var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == cartItem.ProductId);
            if (existingItem == null)
            {
                return null;
            }
             if(existingItem.Quantity > 1)
            {
                existingItem.Quantity--;

                await _cartRepository.SaveChanges();

                return new ResponseItemDto
                {
                    CartId = existingItem.Id,
                    ProductId = cartItem.ProductId,
                    Quantity = existingItem.Quantity,
                };
            }
             await _cartItemsReposirory.remove(existingItem);
         

            return new ResponseItemDto
            {
                CartId = existingItem.Id,
                ProductId = cartItem.ProductId,
                Quantity = existingItem.Quantity,
            };

        }


        public async Task<getCartDto> getCartByUserId(Guid userId)
        {

            var cacheKey = $"cart:{userId}";

            var cachedCart = await _cache.GetAsync<Cart>(cacheKey);

           

            if (cachedCart != null) return JsonSerializer.Deserialize<getCartDto>(cachedCart);


            var cart = await _cartRepository.GetCartByUserId(userId);

            if (cart is null)
            {
                return null;
            }

            List<CartItemDto> cartItems = new List<CartItemDto>();

            foreach (var item in cart.Items)
            {
                CartItemDto mapped = new CartItemDto
                {
                    CartId = item.CartId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    product = new ProductDtos
                    {
                        Id = item.Product.Id,

                        Name = item.Product.Name,

                        CategoryId = item.Product.CategoryId,

                        IsAvailable = item.Product.IsAvailable,

                        PricePerKg = item.Product.PricePerKg,

                        StorageCondition = item.Product.StorageCondition,

                        Type= item.Product.Type,

                        Weight = item.Product.Weight,                         
                    },
                    
                };

                cartItems.Add(mapped);                 

            }

              var dtoCart = new getCartDto
                                         {
                              Id = cart.Id,
                             Items = cartItems,
                             TotalPrice = cart.GetTotalPrice(),
 
                                          };


            await _cache.SetAsync(cacheKey, dtoCart, TimeSpan.FromHours(24));

            return dtoCart;
        }

        public async Task<IEnumerable<object>> removeItemInCart(Guid userId, CartItem existingItem)
        {
            throw new NotImplementedException();
        }
    }
}
