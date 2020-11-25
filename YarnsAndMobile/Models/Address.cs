using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YarnsAndMobile.Models
{
    public class Address
    {
        public int Id { get; set; }
        [Required]
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Zip { get; set; }
    }
}
