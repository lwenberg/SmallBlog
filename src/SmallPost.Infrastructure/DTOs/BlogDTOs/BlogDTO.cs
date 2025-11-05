using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTOs.BlogDTOs
{
    public class BlogDTO
    {

        public int Id { get; set; }
        [MaxLength(100), MinLength(3), Required]
        public string? Title { get; set; }
        [MinLength(3), Required, DataType(DataType.Text)]
        public string? Body { get; set; }
        [DataType(DataType.Date), Display(Name = "Publication Date")]
        public DateTime PubDate { get; set; }

        [MinLength(3)]
        public string? Author { get; set; }
    }
}
