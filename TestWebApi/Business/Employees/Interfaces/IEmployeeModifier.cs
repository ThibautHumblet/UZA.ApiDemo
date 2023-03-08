using TestWebApi.Models.Crud;
using TestWebApi.Models.Dto;
using TestWebApi.Models.Result;

namespace TestWebApi.Business.Employees.Interfaces
{
    public interface IEmployeeModifier
    {
        Task<ItemResult<EmployeeDto>> SaveEmployeeAsync(string code, SaveEmployeeIn request);
    }
}
