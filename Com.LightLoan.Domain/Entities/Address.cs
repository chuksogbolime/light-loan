using System;
using System.Collections.Generic;

namespace Com.LightLoan.Domain.Entities
{
    public class Address
    {
        public Address(){
            Branches = new HashSet<Branch>();
        }
        public long Id {get;set;}
        public string Line1 {get;set;}
        public string Line2 {get;set;}
        public string City {get;set;}
        public string Country {get;set;}
        public char Status {get;set;} 
        public ICollection<Branch> Branches {get; private set;}

    }
}
