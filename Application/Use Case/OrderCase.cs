using Application.OrderDto;
using Domain.Entities;
using Domain.IReposotory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Use_Case
{
    
    public class OrderCase
    {
        private readonly IOrder _orderRepository;

        private readonly ICart _cartRepository;
        public OrderCase(IOrder orderRepository, ICart cartRepository)
        {
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;   
        }

        
        public async Task<CreateOrderDto> CreateOrder(CreateOrderDto orderData)
        {
            var userCart = await _cartRepository.GetCartByUserId(orderData.UserId);

            if (userCart == null)
            {
                return null;
            }

            var order = new Order
            {
                Id = new Guid(),
                
                UserId = orderData.UserId,

                Address = orderData.Address,

                deliveryMethod = orderData.deliveryMethod,

                paymentMethod = orderData.paymentMethod,

                PhoneNumber = orderData.PhoneNumber,
            };

            foreach (var cartItem in userCart.Items)
            {
                order.Items.Add(new OrderItem
                {
                    ProductId = cartItem.ProductId,
                    OrderId = order.Id,
                    Quantity = cartItem.Quantity,
                    UnitPrice = cartItem.Product.PricePerKg,
                    
                });
            }

            _orderRepository.Add(order);

            await _orderRepository.saveChanges();

            return orderData;
        }


        public async Task<List<GetOrdersDto>> GetAllUserOrders(Guid userId)
        {
            var orders = await _orderRepository.GetOrdersByUserId(userId);

            if(orders == null)
            {
                return null;
                
            }
            List<GetOrdersDto> mappedOrders = new List<GetOrdersDto>();

            foreach (var order in orders)
            {
                var mappedOrder = new GetOrdersDto
                {
                   Address = order.Address,
                   deliveryMethod = order.deliveryMethod,
                   paymentMethod = order.paymentMethod, 
                   PhoneNumber = order.PhoneNumber,
                   Items = order.Items,
                   Status = order.Status,
                };

                mappedOrders.Add(mappedOrder);
            }

            return mappedOrders;
        }

        public  async Task<GetOrdersDto> ChangeStatusOrder(Guid userId, byte status)
        {
            var order = await _orderRepository.GetOrderByUserId(userId);

            if(order == null)
            { 
                 return null ;
            }

            switch (status)
            {
               case 0:
                    order.Status = Domain.Enum.OrderStatus.Pending;
                   await _orderRepository.saveChanges();
                    break;
               case 1:
                    order.Status= Domain.Enum.OrderStatus.Processing;
                    await _orderRepository.saveChanges();
                    break;
               case 2:
                    order.Status = Domain.Enum.OrderStatus.Sended;
                    await _orderRepository.saveChanges();
                    break;
               case 3:
                    order.Status = Domain.Enum.OrderStatus.Delivered;
                    await _orderRepository.saveChanges();
                    break;
                case 4:
                    order.Status = Domain.Enum.OrderStatus.Cancelled;
                    await _orderRepository.saveChanges();
                    break;
            }

            return  new GetOrdersDto
            {
                Address = order.Address,
                Status = order.Status,
                deliveryMethod = order.deliveryMethod,
                Items = order.Items,
                paymentMethod = order.paymentMethod,
                PhoneNumber = order.PhoneNumber 
            };
          
        }
    }

}
