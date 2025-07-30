using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace blog_e18.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public string thumbnail { get; set; }

        // Foreign Key properties
        public int CategoryId { get; set; }
        // Navigation properties
        public Category category { get; set; }

        public IEnumerable<Tag> tags { get; set; }

        [NotMapped]
        public IFormFile imgFile { get; set; }
    }
}