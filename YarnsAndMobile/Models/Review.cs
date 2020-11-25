using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YarnsAndMobile.Models
{
    public class Review
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Body { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int Rating { get; set; }
        [Required]
        public virtual Member Member { get; set; }
        [Required]
        public virtual Book Book { get; set; }
    }
}
