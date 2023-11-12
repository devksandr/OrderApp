using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrderApp.Models.ValidateModels;
using OrderApp.Validators.Interfaces;

namespace OrderApp.Validators
{
    public class OrderValidator : IOrderValidator
    {
        public OrderValidateModel Validate(ModelStateDictionary ModelState, Func<bool> action)
        {
            var status = "failure";
            var errors = ModelState.SelectMany(ms => ms.Value.Errors).Select(error => error.ErrorMessage).Distinct().ToList();

            if (ModelState.IsValid)
            {
                var actionResult = action();
                if (!actionResult)
                {
                    status = "failure";
                    var actionError = "Server side error";
                    errors.Insert(0, actionError);
                }
                else
                {
                    status = "success";
                }
            }

            var result = new OrderValidateModel { Status = status, FormErrors = errors };
            return result;
        }
    }
}
