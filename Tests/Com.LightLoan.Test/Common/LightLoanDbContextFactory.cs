using System.Net.Http.Headers;
using System;
using Com.LightLoan.Application.Interface;
using Com.LightLoan.Persistence;
using Moq;
using Microsoft.EntityFrameworkCore;


namespace Com.LightLoan.Test.Common
{
    public static class LightLoanDbContextFactory
    {
        public static LightLoanDbContext Create()
        {
            var dateTime = new DateTime(3001, 1, 1);
            var _datetimeServiceMock = new Mock<IDatetimeService>();
            _datetimeServiceMock.Setup(m=>m.Now).Returns(dateTime);

            var userId=Guid.NewGuid();
            var _userServiceMock=new Mock<IUserService>();
            _userServiceMock.Setup(m=>m.UserId).Returns(userId);

            var options= new DbContextOptionsBuilder<LightLoanDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            var _dbContext=new LightLoanDbContext(_userServiceMock.Object,_datetimeServiceMock.Object,options);
            _dbContext.Database.EnsureCreated();

            return _dbContext;
        }

        public static void Destroy(LightLoanDbContext dbContext)
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }
    }
}