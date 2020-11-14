using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YarnsAndMobile.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime Date { get; set; }
        public int Rating { get; set; }
        public virtual Member Member { get; set; }
        public virtual Book Book { get; set; }
    }
}
