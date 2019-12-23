using System;
using MediatR;

namespace Com.LightLoan.ApplicationService.Branch.DAL.Command.Create
{
    public class CreateBranchCommand :IRequest<bool>
    {
        public int Id {get;set;}
    }
}
