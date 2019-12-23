using System;

namespace Com.LightLoan.Domain.Entities
{
    public class Branch : AuditableEntity
    {
        public Guid Id {get; set;}
        public string Name { get; set; }        
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public long AddressId {get;set;}
        public Address Address { get; set; }
    }
}