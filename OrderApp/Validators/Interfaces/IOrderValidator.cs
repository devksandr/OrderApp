using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrderApp.Models.ValidateModels;

namespace OrderApp.Validators.Interfaces
{
    public interface IOrderValidator
    {
        public OrderValidateModel Validate(ModelStateDictionary ModelState, Func<bool> action);
    }
}
