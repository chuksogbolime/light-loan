using System;

namespace Com.LightLoan.Domain.Entities
{
    public class AuditableEntity
    {
        public Guid CreatedBy {get;set;}
        public DateTime CreatedDate {get;set;}
        public DateTime CreatedDateTime {get;set;}
        public Guid? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public DateTime? LastModifiedDateTime { get; set; }
        public char Status { get; set; }
        public string HashValue1 {get;set;}
        public string HashValue2 {get;set;}

    }
}