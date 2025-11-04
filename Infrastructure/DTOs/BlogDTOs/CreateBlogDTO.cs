using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTOs.BlogDTOs
{
    public class CreateBlogDTO
    {
        [Required, MinLength(3), MaxLength(100)]
        public string? Title { get; set; }
        [Required, MinLength(3)]
        public string? Body { get; set; }
        [Required]
        public DateTime PubDate { get; set; } = DateTime.Now;
        public string? Author { get; set; }
    }
}
