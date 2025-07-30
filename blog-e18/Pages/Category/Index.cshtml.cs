using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blog_e18.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace blog_e18.Pages.Category
{
    public class Index(AppDBContext _db) : PageModel
    {
        public IEnumerable<Models.Category> Categories { get; set; }

        public async Task OnGet()
        {
            Categories = await _db.Categories.ToListAsync();
        }

        public async Task<IActionResult> OnPost(int id)
        {
            var cate = await _db.Categories.FindAsync(id);
            if (cate != null)
            {
                _db.Categories.Remove(cate);
                await _db.SaveChangesAsync();
            }
            return RedirectToPage();
        }
    }
}