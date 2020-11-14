using System;
using System.Collections.Generic;
using System.Text;

namespace YarnsAndMobile.Models
{
    public class Member
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AccountNumber { get; set; }
        public virtual Address Address { get; set; }
        public virtual IEnumerable<Phone> Phones { get; set; }
        public virtual IEnumerable<Sale> Sales { get; set; }
        public virtual IEnumerable<Review> Reviews { get; set; }
    }
}