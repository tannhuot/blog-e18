using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blog_e18.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;

namespace blog_e18.Pages.Category
{
    public class Create(AppDBContext _db) : PageModel
    {
        [BindProperty]
        public Models.Category Category { get; set; }

        public string IsNameValid { get; set; } = "";

        public void OnGet()
        {
        }

        public async Task<ActionResult> OnPost()
        {
            ModelState.Remove("Cateogry.posts");

            if (ModelState.IsValid)
            {
                Console.WriteLine(Category.Name);
                await _db.Categories.AddAsync(Category);
                await _db.SaveChangesAsync();
                return RedirectToPage("index");
            }

            IsNameValid = "is-invalid";

            return Page();
        }
    }

}