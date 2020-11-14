using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YarnsAndMobile.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
        public virtual Member Member { get; set; }
        public virtual Book Book { get; set; }

    }
}
