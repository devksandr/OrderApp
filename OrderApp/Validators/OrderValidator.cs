using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrderApp.Models.DTO.Order;
using OrderApp.Models.ValidateModels;
using OrderApp.Services.Interfaces;
using OrderApp.Validators.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OrderApp.Validators
{
    public class OrderValidator : IOrderValidator
    {
        IOrderService _orderService;

        public OrderValidator(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public OrderValidateModel Validate(ModelStateDictionary modelState, Func<OrderGetResponseDTO, bool> action, OrderGetResponseDTO orderData)
        {
            var status = "failure";
            var errors = new List<string>();

            errors = ValidateModel(modelState);
            if (errors.Count > 0) { return new OrderValidateModel { Status = status, FormErrors = errors }; }

            errors = ValidateUniqueNumberForProvider(orderData);
            if (errors.Count > 0) { return new OrderValidateModel { Status = status, FormErrors = errors }; }

            errors = ValidateMatchNumberWithItemNames(orderData);
            if (errors.Count > 0) { return new OrderValidateModel { Status = status, FormErrors = errors }; }

            errors = ValidateAction(action, orderData);
            if (errors.Count > 0) { return new OrderValidateModel { Status = status, FormErrors = errors }; }

            status = "success";
            return new OrderValidateModel { Status = status, FormErrors = errors };
        }

        private List<string> ValidateModel(ModelStateDictionary modelState) 
            => modelState.SelectMany(ms => ms.Value.Errors).Select(error => error.ErrorMessage).Distinct().ToList();

        private List<string> ValidateUniqueNumberForProvider(OrderGetResponseDTO orderData)
        {
            var errors = new List<string>();
            var conditionalOrders = _orderService.GetOrdersByNumberAndProviderId(orderData.Number, orderData.ProviderId).ToList();

            if (conditionalOrders.Count == 0)
            {
                return errors;
            }

            if (conditionalOrders.Count == 1 && conditionalOrders[0].Id == orderData.Id)
            {
                return errors;
            }

            var numberNotUniqueError = "Current provider already has order with such Number";
            errors.Add(numberNotUniqueError);
            return errors;
        }

        private List<string> ValidateMatchNumberWithItemNames(OrderGetResponseDTO orderData)
        {
            var errors = new List<string>();
            var isMatch = orderData.Items.Any(oi => oi.Name == orderData.Number);

            if (isMatch) 
            {
                var numberMatchError = "Current Number cannot match with current order item Names";
                errors.Add(numberMatchError);
            }
            return errors;
        }

        private List<string> ValidateAction(Func<OrderGetResponseDTO, bool> action, OrderGetResponseDTO orderData)
        {
            var errors = new List<string>();
            var actionResult = action(orderData);
            if (!actionResult)
            {
                var actionError = "Server side error";
                errors.Add(actionError);
            }
            return errors;
        }
    }
}
