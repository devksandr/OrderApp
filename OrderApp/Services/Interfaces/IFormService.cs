using OrderApp.Models.DTO.Form;

namespace OrderApp.Services.Interfaces
{
    public interface IFormService
    {
        FormGetMainPageDataResponseDTO GetDataToShowMainPage();
    }
}
