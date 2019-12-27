using System;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Moq;
using Com.LightLoan.Application.Interface;
using Com.LightLoan.Persistence;
using System.Linq;
using System.Threading.Tasks;
using Com.LightLoan.Domain.Entities;

namespace Com.LightLoan.Test.Persistence
{
    [TestClass]
    public class LightLoanDbContextTest : IDisposable
    {
        readonly Mock<IDatetimeService> _datetimeServiceMock;
        readonly Mock<IUserService> _userServiceMock;
        readonly LightLoanDbContext _dbContext;
        readonly Guid userId;
        private readonly DateTime dateTime;
        readonly Guid testId;
        public LightLoanDbContextTest()
        {
            dateTime = new DateTime(3001, 1, 1);
            _datetimeServiceMock = new Mock<IDatetimeService>();
            _datetimeServiceMock.Setup(m=>m.Now).Returns(dateTime);

            userId=Guid.NewGuid();
            _userServiceMock=new Mock<IUserService>();
            _userServiceMock.Setup(m=>m.UserId).Returns(userId);

            var options= new DbContextOptionsBuilder<LightLoanDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            _dbContext=new LightLoanDbContext(_userServiceMock.Object,_datetimeServiceMock.Object,options);

            testId=Guid.NewGuid();
            Branch branch=new Branch{
                Id= testId, Name="Test Branch For Change", 
                Address = new Address {
                    Id=1,
                    Line1="Line1 Test",
                    Line2="Line2 Test",
                    City="City Test",
                    Country="Country Test",
                    Status=(char)Domain.Enums.Status.New
                }, 
                Phone1="Test Phone 1", Phone2="Test Phone 2",
                HashValue1="Test Hash value 1", HashValue2="Test Hash value 2"
            };
            _dbContext.Branches.Add(branch);
            _dbContext.SaveChanges();

        }
        [TestMethod]
        public async Task SaveChangesAsync_Should_Set_CreatedProperties_on_Entity_Add()
        {
            //Arrange
            Branch branch=new Branch{
                Id=Guid.NewGuid(),Name="Test Branch", 
                Address = new Address {
                    Id=2,
                    Line1="Line1 Test",
                    Line2="Line2 Test",
                    City="City Test",
                    Country="Country Test",Status=(char)Domain.Enums.Status.New
                }, Phone1="Test Phone 1", Phone2="Test Phone 2",
                HashValue1="Test Hash value 1", HashValue2="Test Hash value 2"
            };
            //Act
            _dbContext.Branches.Add(branch);
            await _dbContext.SaveChangesAsync();
            char expectedStatus=(char)Domain.Enums.Status.New;
            //Assert
            branch.CreatedBy.ShouldBe(userId);
            branch.CreatedDate.ShouldBe(dateTime.Date);
            branch.CreatedDateTime.ShouldBe(dateTime);
            branch.LastModifiedBy.ShouldBeNull();
            branch.LastModifiedDate.ShouldBeNull();
            branch.LastModifiedDateTime.ShouldBeNull();
            branch.Status.ShouldBe(expectedStatus);

        }

        [TestMethod]
        public async Task SaveChangesAsync_Should_Set_Modified_Properties_on_Entity_Modified()
        {
            //Arrange
            Branch thisBranch= await _dbContext.Branches.FindAsync(testId);            
            //Act
            thisBranch.Name="Updated Branch";
            await _dbContext.SaveChangesAsync();
            //Assert
            thisBranch.LastModifiedBy.ShouldNotBeNull();
            thisBranch.LastModifiedDate.ShouldBe(dateTime.Date);
            thisBranch.LastModifiedDateTime.ShouldBe(dateTime);
        }

        [TestMethod]
        public async Task SaveChangesAsync_should_add_child_entity_on_parent_creation()
        {
            //Arrange
            long id=1;            
            //Act
            Address address = await _dbContext.Addresses.FindAsync(id);
            //Assert
            address.ShouldNotBeNull();
        }

        /*[TestMethod]
        public async Task SaveChangesAsync_Should_Set_Modified_Properties_and_Status_on_Entity_Deleted()
        {
            //Arrange
            Branch thisBranch= await _dbContext.Branches.FindAsync(testId);            
            //Act
            _dbContext.Branches.Remove(thisBranch);
            await _dbContext.SaveChangesAsync();
            //Assert
            thisBranch.LastModifiedBy.ShouldNotBeNull();
            thisBranch.LastModifiedDate.ShouldBe(dateTime.Date);
            thisBranch.LastModifiedDateTime.ShouldBe(dateTime);
        }*/

        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}