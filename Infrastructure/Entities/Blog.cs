using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using System.Text;

namespace Infrastructure.Entities
{
    public class Blog
    {
        public int Id { get; set; }
        [MaxLength(100), MinLength(3)]
        public string? Title { get; set; }
        [MinLength(3), Required, DataType(DataType.Text)]
        public string? Body { get; set; }
        [DataType(DataType.Date), Display(Name = "Publication Date")]
        public DateTime PubDate { get; set; }
    }
}
