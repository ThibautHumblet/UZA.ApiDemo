using Microsoft.EntityFrameworkCore;
using TestWebApi.Business.Employees.Interfaces;
using TestWebApi.Integration.Database;
using TestWebApi.Integration.Database.Entities;
using TestWebApi.Models.Dto;
using TestWebApi.Models.Enum;
using TestWebApi.Models.Query;
using TestWebApi.Models.Result;

namespace TestWebApi.Business.Employees
{
    public class EmployeeRetriever : IEmployeeRetriever
    {
        private HospitalDBContext Context { get; }
        public EmployeeRetriever(HospitalDBContext context)
        {
            Context = context;
        }

        public async Task<ListResult<EmployeeDto>> FindEmployeesAsync(FindEmployeeQuery request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var employees = await GetQuery(request).Select(employee =>
                new EmployeeDto
                {
                    EmployeeCode = employee.Code,
                    Firstname = employee.FirstName,
                    Lastname = employee.LastName,
                    Birthdate = employee.BirthDate,
                    Sex = employee.Sex,
                    Inactive = employee.Inactive,
                }).ToListAsync();

            if (employees == null)
            {
                return new ListResult<EmployeeDto>
                {
                    Message = "Er is iets verkeerd gegaan, probeer het later opnieuw.",
                    ResultType = ResultType.BadRequest,
                    Result = null
                };
            }

            if (employees.Count < 1)
            {
                return new ListResult<EmployeeDto>
                {
                    Message = "Er werden geen werknemers gevonden.",
                    ResultType = ResultType.NotFound,
                    Result = null
                };
            }

            return new ListResult<EmployeeDto>
            {
                Message = null,
                ResultType = ResultType.Found,
                Result = employees
            };
        }

        private IQueryable<Employee> GetQuery(FindEmployeeQuery request)
        {
            IQueryable<Employee> query = Context.Employees;

            if (!string.IsNullOrEmpty(request.EmployeeCode))
            {
                query = query.Where(entity => entity.Code.Contains(request.EmployeeCode));
            }
            if (!string.IsNullOrEmpty(request.Firstname))
            {
                query = query.Where(entity => entity.FirstName.Contains(request.Firstname));
            }
            if (!string.IsNullOrEmpty(request.Lastname))
            {
                query = query.Where(entity => entity.LastName.Contains(request.Lastname));
            }
            if (request.FromBirthDate.HasValue)
            {
                query = query.Where(entity => entity.BirthDate >= request.FromBirthDate.GetValueOrDefault());
            }
            if (request.ToBirthDate.HasValue)
            {
                query = query.Where(entity => entity.BirthDate <= request.ToBirthDate.GetValueOrDefault());
            }
            if (!string.IsNullOrEmpty(request.Sex))
            {
                query = query.Where(entity => entity.Sex.Equals(request.Sex));
            }
            if (request.Inactive.HasValue)
            {
                query = query.Where(entity => entity.Inactive.Equals(request.Inactive));
            }

            return query;
        }
    }
}
