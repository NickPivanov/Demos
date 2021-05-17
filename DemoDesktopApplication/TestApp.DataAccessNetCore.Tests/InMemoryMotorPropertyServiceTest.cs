using DataAccess.NetCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TestApp.DataAccessNetCore.Tests.Services;

namespace TestApp.DataAccessNetCore.Tests
{
    public class InMemoryMotorPropertyServiceTest : MotorPropertyServiceTest
    {
        public InMemoryMotorPropertyServiceTest() : base(
            new DbContextOptionsBuilder<MotorDbContext>()
            .UseInMemoryDatabase("MotorsDb").Options)
        { }
    }
}
