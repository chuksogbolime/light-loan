using System;
using MediatR;

namespace Com.LightLoan.ApplicationService.BranchService.DAL.Command.Create
{
    public class CreateBranchCommand :IRequest<Guid>
    {
        public string Name { get; set; }        
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string AddressLine1 {get;set;}
        public string AddressLine2 {get;set;}
        public string City {get;set;}
        public string Country {get;set;}
    }
}
