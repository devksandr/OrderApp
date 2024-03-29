﻿using Microsoft.AspNetCore.Mvc;
using OrderApp.Models.DTO.Order;
using OrderApp.Services.Interfaces;
using OrderApp.Validators.Interfaces;
using System;

namespace OrderApp.Controllers
{
    public class OrderController : Controller
    {
        IOrderService _orderService;
        IOrderValidator _orderValidator;

        public OrderController(IOrderService orderService, IOrderValidator orderValidator)
        {
            _orderService = orderService;
            _orderValidator = orderValidator;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(OrderDTO orderData)
        {
            Func<OrderDTO, bool> action = _orderService.CreateOrder;
            var result = _orderValidator.Validate(ModelState, action, orderData);
            return Json(result);
        }

        [HttpPut]
        public IActionResult Update(OrderDTO orderData)
        {
            Func<OrderDTO, bool> action = _orderService.UpdateOrder;
            var result = _orderValidator.Validate(ModelState, action, orderData);
            return Json(result);
        }

        [HttpDelete("/Order/Delete/{orderId}")]
        public void Delete(int orderId)
        {
            var result = _orderService.DeleteOrder(orderId);
        }
    }
}
