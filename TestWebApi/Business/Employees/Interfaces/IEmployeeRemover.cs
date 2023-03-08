using TestWebApi.Models.Dto;
using TestWebApi.Models.Result;

namespace TestWebApi.Business.Employees.Interfaces
{
    public interface IEmployeeRemover
    {
        Task<ItemResult<EmployeeDto>> RemoveAsync(string code);
    }
}
