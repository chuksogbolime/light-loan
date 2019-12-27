using System;
using Com.LightLoan.Persistence;

namespace Com.LightLoan.Test.Common
{
    public class CommandTestBase : IDisposable
    {
        public LightLoanDbContext _dbContext {get;}

        public CommandTestBase()
        {
            _dbContext=LightLoanDbContextFactory.Create();
        }

        public void Dispose()
        {
            LightLoanDbContextFactory.Destroy(_dbContext);
        }
    }
}
