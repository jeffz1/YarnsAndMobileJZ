using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YarnsAndMobile.Models
{
    public class Phone
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public PhoneType Type { get; set; }
        public Member Member { get; set; }
    }
}
