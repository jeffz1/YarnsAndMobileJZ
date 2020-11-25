using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YarnsAndMobile.Models
{
    public class Member : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string AccountNumber { get; set; }
        public virtual Address Address { get; set; }
        public virtual IEnumerable<Phone> Phones { get; set; }
        public virtual IEnumerable<Sale> Sales { get; set; }
        public virtual IEnumerable<Review> Reviews { get; set; }

    }
}