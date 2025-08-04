using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

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
        public Category Category { get; set; }

        public ICollection<Tag> Tags { get; set; }

        [NotMapped]
        public IFormFile imgFile { get; set; }
    }
}