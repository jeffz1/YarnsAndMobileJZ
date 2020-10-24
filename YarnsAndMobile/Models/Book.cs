using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YarnsAndMobile.Models
{
    public class Book
    {
        public int BookId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        public short CopyrightYear { get; set; }
        public string ISBN { get; set; }
        public string ImageURL { get; set; }
    }
}
