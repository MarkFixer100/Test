using Domain.Entities;
using Domain.IReposotory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Use_Case
{
    public class CartCase
    {
        private readonly ICart _cartRepository;

        public CartCase(ICart cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<CartItem> addItemInCart(Guid userId , CartItem cartItem)
        {
            Cart cart = await _cartRepository.GetCartByUserId(userId);


            var productInCartitem = cart.Items.FirstOrDefault(p => p.ProductId == cartItem.ProductId);

            if (productInCartitem == null)
            {
                _cartRepository.AddCartItem(cartItem, cart);

               await _cartRepository.SaveChanges();

                return cartItem;
            }



            productInCartitem.Quantity++;

             await _cartRepository.SaveChanges();

            return productInCartitem;

        }

        public async Task<CartItem?> removeItemInCart(Guid userId, CartItem cartItem)
        {
            var cart = await _cartRepository.GetCartByUserId(userId);

            var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == cartItem.ProductId);
            if (existingItem == null)
            {
                return null;
            }
             

            _cartRepository.RemoveCartItem(existingItem, cart);
            await _cartRepository.SaveChanges();

            return existingItem;
        }


        public async Task<Cart> getCartByUserId(Guid userId)
        {
            var cart = await _cartRepository.GetCartByUserId(userId);

            return cart; 
        
        }


    }
}
