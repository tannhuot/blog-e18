using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;
using blog_e18.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace blog_e18.Pages.Post
{
    public class Create(AppDBContext _db, IWebHostEnvironment _environment) : PageModel
    {

        [BindProperty]
        public Models.Post post { get; set; }

        [BindProperty]
        public int SelectedCategoryId { get; set; }

        public SelectList CategoryItems { get; set; }

        public IEnumerable<Models.Tag> Tags { get; set; }

        [BindProperty]
        public List<int> SelectedTags { get; set; } = new List<int>();

        public string TitleError { get; set; } = "";

        public async Task OnGet()
        {
            IEnumerable<Models.Category> categories = await _db.Categories.ToListAsync();
            CategoryItems = new SelectList(categories, nameof(Models.Category.Id), nameof(Models.Category.Name));

            Tags = await _db.Tags.ToListAsync();
        }

        public async Task<IActionResult> OnPost()
        {

            ModelState.Remove("post.thumbnail");
            ModelState.Remove("post.category");
            ModelState.Remove("post.tags");
            ModelState.Remove("post.imgFile");

            if (ModelState.IsValid)
            {
                if (post.imgFile != null && post.imgFile.Length > 0)
                {
                    // Get the directory "wwwroot"
                    var uploadFolder = Path.Combine(_environment.WebRootPath, "uploads");

                    // Create folder "uploads" if not exist
                    if (!Directory.Exists(uploadFolder))
                    {
                        Directory.CreateDirectory(uploadFolder);
                    }

                    // Create unique file name
                    var fileName = Path.GetFileNameWithoutExtension(post.imgFile.FileName);
                    var extension = Path.GetExtension(post.imgFile.FileName);
                    var uploadFileName = $"{fileName}_{Guid.NewGuid()}{extension}";

                    var filePath = Path.Combine(uploadFolder, uploadFileName);

                    // Save file
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await post.imgFile.CopyToAsync(fileStream);
                    }

                    // Set file name to DB
                    post.thumbnail = uploadFileName;

                    // one-to-many with tbl_Category
                    var cate = _db.Categories.Include(c => c.Posts).First(c => c.Id == SelectedCategoryId);
                    cate.Posts.Add(post);


                    //Many-to-Many
                    post.Tags = [];
                    foreach (var tagId in SelectedTags)
                    {
                        var tag = await _db.Tags.FindAsync(tagId);
                        post.Tags.Add(tag);
                    }

                    await _db.Posts.AddAsync(post);

                    _db.SaveChanges();

                    return RedirectToPage("/post/index");

                }
            }

            TitleError = "is-invalid";

            await OnGet();
            return Page();
        }
    }
}