using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YarnsAndMobile.Models
{
    public class Sale
    {
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "decimal(5,2)")]
        public decimal Price { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public virtual Member Member { get; set; }
        [Required]
        public virtual Book Book { get; set; }

    }
}
