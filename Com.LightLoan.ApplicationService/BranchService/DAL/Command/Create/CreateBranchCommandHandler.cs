using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Com.LightLoan.Application.Interface;
using Com.LightLoan.Domain.Entities;

namespace Com.LightLoan.ApplicationService.BranchService.DAL.Command.Create
{
    public class CreateBranchCommandHandler : IRequestHandler<CreateBranchCommand, Guid>
    {
        private readonly ILightLoanDbContext _dbContext;

        public CreateBranchCommandHandler(ILightLoanDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> Handle(CreateBranchCommand request, CancellationToken cancellationToken)
        {
            Branch branch = new Branch
            {
                Name = request.Name,
                Address = new Address
                {
                    Line1 = request.AddressLine1,
                    Line2 = request.AddressLine2,
                    City = request.City,
                    Country = request.Country,
                    Status = (char)Domain.Enums.Status.New
                },
                Phone1 = request.Phone1,
                Phone2 = request.Phone2
            };
            branch.Id = Guid.NewGuid();  
            _dbContext.Branches.Add(branch);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return branch.Id;
        }
    }
}
