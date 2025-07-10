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

namespace Tests;

public class UnitTest1
{
    [Fact]
    public async Task AddItemInCart_ShouldAddNewItem_WhenNotExistsInCart()
    {
        Console.WriteLine("Test started");
        // Arrange
        var userId = Guid.NewGuid();
        var productId = Guid.NewGuid();

        var newItem = new CartItem
        {
            ProductId = productId,
            Quantity = 1
        };

        var cart = new Cart
        {
            UserId = userId,
            Items = new List<CartItem>() // пуста€ корзина
        };

        var mockCartRepo = new Mock<ICart>();
        mockCartRepo.Setup(r => r.GetCartByUserId(userId)).ReturnsAsync(cart);
        mockCartRepo.Setup(r => r.SaveChanges()).Returns(Task.CompletedTask);

        var cartService = new CartCase(mockCartRepo.Object); // сервис, который содержит метод AddItemInCart

        // Act
        var result = await cartService.addItemInCart(userId, newItem);

        // Assert
        mockCartRepo.Verify(r => r.AddCartItem(newItem, cart), Times.Once);
        mockCartRepo.Verify(r => r.SaveChanges(), Times.Once);
        Assert.Equal(productId, result.ProductId);
        Assert.Equal(1, result.Quantity);
    }
}