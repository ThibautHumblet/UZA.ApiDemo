using TestWebApi.Integration.Database.Entities;
using TestWebApi.Models.Dto;
using TestWebApi.Models.Query;
using TestWebApi.Models.Result;

namespace TestWebApi.Business.Employees.Interfaces
{
    public interface IEmployeeRetriever
    {
        Task<ListResult<EmployeeDto>> FindEmployeesAsync(FindEmployeeQuery request);
    }
}
