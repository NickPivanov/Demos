using DataAccess;
using HRAgency.HRAgencyAPI.Tests.Services;
using Microsoft.EntityFrameworkCore;

namespace HRAgency.HRAgencyAPI.Tests
{
    public class InMemoryPersonelExpensesServiceTest : PersonelExpensesServiceTest
    {
        public InMemoryPersonelExpensesServiceTest() : base(
            new DbContextOptionsBuilder<HRAgencyDBContext>()
            .UseInMemoryDatabase("Data Source=test.db").Options)
        {

        }
    }
}
