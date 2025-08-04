using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blog_e18.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace blog_e18.Pages.Post
{
    public class Index(AppDBContext _db) : PageModel
    {

        public IEnumerable<Models.Post> posts { get; set; }

        public async Task OnGet()
        {
            posts = await _db.Posts.Include(p => p.Category).Include(p => p.Tags).ToListAsync();
        }
    }
}