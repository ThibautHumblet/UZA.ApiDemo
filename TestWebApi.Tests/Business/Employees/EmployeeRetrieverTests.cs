using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebApi.Business.Employees;
using TestWebApi.Integration.Database;
using TestWebApi.Integration.Database.Entities;
using TestWebApi.Models.Dto;
using TestWebApi.Models.Enum;
using TestWebApi.Models.Query;
using TestWebApi.Models.Result;

namespace TestWebApi.Tests.Business.Employees
{
    public class EmployeeRetrieverTests : TestInMemoryDb
    {
        public EmployeeRetrieverTests() : base(new DbContextOptionsBuilder<HospitalDBContext>()
            .UseInMemoryDatabase(databaseName: "EmployeeRetrieverInMemoryDb")
            .Options)
        { }

        [Fact]
        public async void FindAsync_ReturnNotFound_Null()
        {
            using var context = new HospitalDBContext(ContextOptions);
            var request = new FindEmployeeQuery
            {
                Firstname = "willneverfind"
            };
            var retriever = new EmployeeRetriever(context);

            ListResult<EmployeeDto> result = await retriever.FindEmployeesAsync(request);

            Assert.Null(result.Result);
            Assert.Equal(ResultType.NotFound, result.ResultType);
        }

        [Fact]
        public async void FindAsync_ReturnSomething_EmployeeDto()
        {
            using var context = new HospitalDBContext(ContextOptions);
            var request = new FindEmployeeQuery();
            var retriever = new EmployeeRetriever(context);

            ListResult<EmployeeDto> result = await retriever.FindEmployeesAsync(request);

            Assert.NotNull(result);
            Assert.Equal(ResultType.Found, result.ResultType);
        }

        [Fact]
        public async void FindAsync_ReturnSpecificItem_PatientDto()
        {
            using var context = new HospitalDBContext(ContextOptions);

            var newDummyEmployee = new Employee() { EmployeeId = -1, Code = "-1", FirstName = "A", LastName = "Z", BirthDate = DateTime.MaxValue, Sex = "U", Inactive = true };
            context.Add(newDummyEmployee);
            context.SaveChanges();

            var request = new FindEmployeeQuery
            {
                EmployeeCode = "1",
                Firstname = "TestFirst"
            };
            var retriever = new EmployeeRetriever(context);

            ListResult<EmployeeDto> result = await retriever.FindEmployeesAsync(request);

            Assert.NotNull(result.Result);
            Assert.Single(result.Result);
            Assert.Equal(ResultType.Found, result.ResultType);
        }
    }
}
