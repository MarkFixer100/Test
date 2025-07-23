using Xunit;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Domain.Entities;
using Domain.IReposotory;
using Application.Use_Case;
using Application.OrderDto;

namespace Tests;

public class CreateOrderTests
{
    [Fact]
    public async Task CreateOrder_ShouldCreateOrder_WhenCartExists()
    {
        // ---------- Arrange ----------
        var userId = Guid.NewGuid();

        var cart = new Cart
        {
            Id = userId,
            Items = new List<CartItem>
            {
                new ()
                {
                    ProductId = Guid.NewGuid(),
                    Quantity  = 2,
                    Product   = new Product { PricePerKg = 19.9m }
                },
                new ()
                {
                    ProductId = Guid.NewGuid(),
                    Quantity  = 1,
                    Product   = new Product { PricePerKg = 42m }
                }
            }
        };

        var orderDto = new CreateOrderDto
        {
            UserId = userId,
            Address = "Some street 12",
            deliveryMethod = "courier",
            paymentMethod = "card",
            PhoneNumber = "+992 900‑000‑000"
        };

        var mockCartRepo = new Mock<ICart>();
        var mockOrderRepo = new Mock<IOrder>();

        mockCartRepo.Setup(r => r.GetCartByUserId(userId))
                    .ReturnsAsync(cart);
        mockOrderRepo.Setup(r => r.saveChanges())
                     .Returns(Task.CompletedTask);

        var service = new OrderCase(mockOrderRepo.Object , mockCartRepo.Object);

        // ---------- Act ----------
        var result = await service.CreateOrder(orderDto);

        // ---------- Assert ----------
        mockOrderRepo.Verify(r => r.Add(It.Is<Order>(o =>
     o.UserId == userId &&                          // теперь сравниваем UserId
     o.Items.Count == cart.Items.Count)), Times.Once);
    }

    [Fact]
    public async Task CreateOrder_ShouldReturnNull_WhenCartDoesNotExist()
    {
        // ---------- Arrange ----------
        var userId = Guid.NewGuid();
        var orderDto = new CreateOrderDto { UserId = userId };

        var mockCartRepo = new Mock<ICart>();
        var mockOrderRepo = new Mock<IOrder>();

        mockCartRepo.Setup(r => r.GetCartByUserId(userId))
                    .ReturnsAsync((Cart?)null);      // корзина не найдена

        var service = new OrderCase(mockOrderRepo.Object, mockCartRepo.Object);

        // ---------- Act ----------
        var result = await service.CreateOrder(orderDto);

        // ---------- Assert ----------
        Assert.Null(result);
        mockOrderRepo.Verify(r => r.Add(It.IsAny<Order>()), Times.Never);
        mockOrderRepo.Verify(r => r.saveChanges(), Times.Never);
    }
}
