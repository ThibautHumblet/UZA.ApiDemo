using Microsoft.EntityFrameworkCore;
using TestWebApi.Business.Employees.Interfaces;
using TestWebApi.Integration.Database;
using TestWebApi.Integration.Database.Entities;
using TestWebApi.Models.Crud;
using TestWebApi.Models.Dto;
using TestWebApi.Models.Enum;
using TestWebApi.Models.Result;

namespace TestWebApi.Business.Employees
{
    public class EmployeeModifier: IEmployeeModifier
    {
        private HospitalDBContext Context { get; }
        public EmployeeModifier(HospitalDBContext context)
        {
            Context = context;
        }

        public async Task<ItemResult<EmployeeDto>> SaveEmployeeAsync(string code, SaveEmployeeIn request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (string.IsNullOrEmpty(code))
            {
                return new ItemResult<EmployeeDto>
                {
                    Message = "Code is leeg",
                    ResultType = ResultType.BadRequest
                };
            }

            var employee = await Context.Employees
                .FirstOrDefaultAsync(item => item.Code == code);

            var modificationResult = new ItemResult<EmployeeDto>();

            if (employee == null)
            {
                modificationResult.ResultType = ResultType.Created;
                employee = new Employee
                {
                    Code = code
                };
                await Context.Employees.AddAsync(employee);
            }
            else
            {
                if (employee.Inactive)
                {
                    return new ItemResult<EmployeeDto>
                    {
                        Message = "Verwijderde employees kunnen niet aangepast worden",
                        ResultType = ResultType.BadRequest
                    };
                }
                modificationResult.ResultType = ResultType.Updated;
            }

            employee.FirstName = request.Firstname;
            employee.LastName = request.Lastname;
            employee.BirthDate = request.Birthdate;
            employee.Sex = request.Sex;

            await Context.SaveChangesAsync();

            modificationResult.Result = new EmployeeDto
            {
                EmployeeCode = employee.Code,
                Firstname = employee.FirstName,
                Lastname = employee.LastName,
                Birthdate = employee.BirthDate,
                Sex = employee.Sex,
                Inactive = false
            };

            return modificationResult;
        }
    }
}
