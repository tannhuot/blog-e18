using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using blog_e18.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace blog_e18.Pages.Category
{
    public class Edit(AppDBContext _db) : PageModel
    {
        [BindProperty]
        public Models.Category Category { get; set; }

        public string IsNameValid { get; set; } = "";

        public async Task OnGet(int id)
        {
            Category = await _db.Categories.FindAsync(id);
        }

        public async Task<IActionResult> OnPost()
        {

            if (ModelState.IsValid)
            {
                var updateCategory = await _db.Categories.FindAsync(Category.Id);
                updateCategory.Name = Category.Name;
                await _db.SaveChangesAsync();

                return RedirectToPage("index");
            }

            IsNameValid = "is-invalid";

            return Page();
        }
    }
}