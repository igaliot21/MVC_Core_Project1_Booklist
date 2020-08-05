using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookList.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(512)]
        public string Name { get; set; }
    }
}
