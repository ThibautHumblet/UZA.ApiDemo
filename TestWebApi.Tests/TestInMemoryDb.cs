using Microsoft.EntityFrameworkCore;
using TestWebApi.Integration.Database;
using TestWebApi.Integration.Database.Entities;

namespace TestWebApi.Tests
{
    public class TestInMemoryDb
    {
        public DbContextOptions<HospitalDBContext> ContextOptions { get; }
        public TestInMemoryDb(DbContextOptions<HospitalDBContext> contextOptions)
        {
            ContextOptions = contextOptions;
            SeedInMemoryDb();
        }

        private void SeedInMemoryDb()
        {
            using var context = new HospitalDBContext(ContextOptions);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var employee = new Employee
            {
                EmployeeId = 1,
                Code = "12345678",
                FirstName = "TestFirst",
                LastName = "TestLast",
                BirthDate = DateTime.MinValue,
                Sex = "M",
                Inactive = false
            };

            context.Add(employee);

            context.SaveChanges();
        }
    }
}
