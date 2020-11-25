using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YarnsAndMobile.Models
{
    public class Book
    {
        public int BookId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        [Display(Name = "Copyright Year")]
        public short CopyrightYear { get; set; }

        public string ISBN { get; set; }

        public string ImageUrl { get; set; }

        [Display(Name = "Price")]
        [Required]
        [Column(TypeName = "decimal(5,2)")]
        public decimal CurrentSalePrice { get; set; }
    }
}
