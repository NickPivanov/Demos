using DataAccess;
using HRAgency.HRAgencyAPI.Tests.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRAgency.HRAgencyAPI.Tests
{
    public class InMemoryEmployeeServiceTest : EmployeeServiceTest
    {
        public InMemoryEmployeeServiceTest() : base(
            new DbContextOptionsBuilder<HRAgencyDBContext>()
            .UseInMemoryDatabase("Data Source=test.db").Options)
        {

        }
    }
}
