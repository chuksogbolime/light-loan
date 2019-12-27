using System;
using Com.LightLoan.Test.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using Com.LightLoan.ApplicationService.BranchService.DAL.Command.Create;
using System.Threading;
using System.Threading.Tasks;

namespace Com.LightLoan.Test.ApplicationService.BranchService.Command
{
    [TestClass]
    public class CreateBranchCommandTest : CommandTestBase
    {
        [TestMethod]
        public async Task Should_Persist_Branch_To_DB_Test()
        {
            //Arrange
            var command = new CreateBranchCommand
            {
                Name = "Create Test Branch",
                Phone1 = "Create Phone1",
                Phone2 = "Create Phone2",
                AddressLine1 = "Create AddressLine1",
                AddressLine2 = "Create AddressLine2",
                City = "Create City",
                Country = "Create Country"

            };

            var handler = new CreateBranchCommandHandler(_dbContext);
            //Act
            var result = await handler.Handle(command, CancellationToken.None);
            var entity = _dbContext.Branches.Find(result);

            //Assert
            
            result.ShouldNotBeNull();
            result.ShouldBeOfType<Guid>();
            entity.ShouldNotBeNull();
            


        }
    }
}
