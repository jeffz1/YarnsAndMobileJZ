using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YarnsAndMobile.Models
{
    public class Phone
    {
        public int Id { get; set; }
        [Required]
        public string Number { get; set; }
        [Required]
        public PhoneType Type { get; set; }
        [Required]
        public Member Member { get; set; }
    }
}
