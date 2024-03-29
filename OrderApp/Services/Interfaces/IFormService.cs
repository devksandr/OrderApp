﻿using OrderApp.Models.DTO.Form;
using OrderApp.Models.DTO.Order;

namespace OrderApp.Services.Interfaces
{
    public interface IFormService
    {
        FormGetMainPageDataResponseDTO GetDataToShowMainPage();
        IEnumerable<FormGetOrderRowResponseDTO> ConvertOrdersToOrderRows(IEnumerable<OrderDTO> orders);
        FormGetDataToCreateOrUpdateOrderResponseDTO GetDataToCreateOrUpdateOrder(string orderId);
    }
}
