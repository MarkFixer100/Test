using Xunit;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Domain.Entities;
using Domain.IReposotory;
using Infostructure.Repository;
using Application.Use_Case;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace Tests
{
    public class RemoveCartItemTests
    {
        [Fact]
        public async Task RemoveItemInCart_ShouldRemoveItem_WhenExistsInCart()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var productId = Guid.NewGuid();

            var existingItem = new CartItem { ProductId = productId, Quantity = 1 };
            var cart = new Cart { UserId = userId, Items = new List<CartItem> { existingItem } };

            var mockRepo = new Mock<ICart>();
            mockRepo.Setup(r => r.GetCartByUserId(userId)).ReturnsAsync(cart);
            mockRepo.Setup(r => r.SaveChanges()).Returns(Task.CompletedTask);

            var cartService = new CartCase(mockRepo.Object);

            // Act
            var result = await cartService.removeItemInCart(userId, existingItem);

            // Assert
            mockRepo.Verify(r => r.RemoveCartItem(existingItem, cart), Times.Once);
            mockRepo.Verify(r => r.SaveChanges(), Times.Once);
            Assert.Equal(productId, result?.ProductId);
        }


        [Fact]
        public async Task RemoveItemInCart_ShouldReturnNull_WhenItemNotFoundInCart()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var requestedItem = new CartItem { ProductId = Guid.NewGuid() };

            var cart = new Cart
            {
                UserId = userId,
                Items = new List<CartItem>() // пустая корзина
            };

            var mockRepo = new Mock<ICart>();
            mockRepo.Setup(r => r.GetCartByUserId(userId)).ReturnsAsync(cart);

            var cartService = new CartCase(mockRepo.Object);

            // Act
            var result = await cartService.removeItemInCart(userId, requestedItem);

            // Assert
            mockRepo.Verify(r => r.RemoveCartItem(It.IsAny<CartItem>(), cart), Times.Never);
            mockRepo.Verify(r => r.SaveChanges(), Times.Never);
            Assert.Null(result);
        }

        [Fact]
        public async Task RemoveItemInCart_ShouldRemoveItemFromCartList()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var itemToRemove = new CartItem { ProductId = Guid.NewGuid() };

            var cart = new Cart
            {
                UserId = userId,
                Items = new List<CartItem> { itemToRemove }
            };

            var mockRepo = new Mock<ICart>();
            mockRepo.Setup(r => r.GetCartByUserId(userId)).ReturnsAsync(cart);
            mockRepo.Setup(r => r.SaveChanges()).Returns(Task.CompletedTask);
            mockRepo.Setup(r => r.RemoveCartItem(itemToRemove, cart))
                    .Callback(() => cart.Items.Remove(itemToRemove)); // вручную удаляем

            var cartService = new CartCase(mockRepo.Object);

            // Act
            var result = await cartService.removeItemInCart(userId, itemToRemove);

            // Assert
            Assert.DoesNotContain(itemToRemove, cart.Items);
            Assert.Empty(cart.Items); // <- это правильная проверка
        }

    }
}
