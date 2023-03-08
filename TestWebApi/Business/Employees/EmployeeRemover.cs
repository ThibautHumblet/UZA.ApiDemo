using Azure.Core;
using Microsoft.EntityFrameworkCore;
using TestWebApi.Business.Employees.Interfaces;
using TestWebApi.Integration.Database;
using TestWebApi.Models.Dto;
using TestWebApi.Models.Enum;
using TestWebApi.Models.Result;

namespace TestWebApi.Business.Employees
{
    public class EmployeeRemover : IEmployeeRemover
    {
        private HospitalDBContext Context { get; }
        public EmployeeRemover(HospitalDBContext context)
        {
            Context = context;
        }

        public async Task<ItemResult<EmployeeDto>> RemoveAsync(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return new ItemResult<EmployeeDto>
                {
                    Message = "Code is leeg",
                    ResultType = ResultType.BadRequest
                };
            }

            var employee = await Context.Employees
                .FirstAsync(item => item.Code == code);

            if (employee == null)
            {
                return new ItemResult<EmployeeDto>
                {
                    ResultType = ResultType.NotFound
                };
            }

            employee.Inactive = true;

            await Context.SaveChangesAsync();

            return new ItemResult<EmployeeDto> { ResultType = ResultType.NoContent };
        }
    }
}
