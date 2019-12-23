using System;

namespace Com.LightLoan.Domain.Entities
{
    public class Audit
    {
        public Guid Id {get;set;}
        public string Name {get; set;}
        public string State {get; set;}
        public Guid ActionBy {get; set;}
        public DateTime ActionDate {get;set;}
        public DateTime ActionDateTime {get;set;}


    }
}