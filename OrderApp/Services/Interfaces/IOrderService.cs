﻿using OrderApp.Models.DTO.Order;

namespace OrderApp.Services.Interfaces
{
    public interface IOrderService
    {
        public IEnumerable<OrderGetResponseDTO> GetAllOrders(bool includeItems);
        public IEnumerable<OrderItemGetResponseDTO> GetAllOrderItems();
    }
}