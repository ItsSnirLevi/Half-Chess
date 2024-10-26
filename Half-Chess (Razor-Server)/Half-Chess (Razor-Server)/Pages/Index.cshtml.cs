using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Half_Chess__Razor_Server_.Data;
using Half_Chess__Razor_Server_.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Half_Chess__Razor_Server_.Pages
{
    public class IndexModel : PageModel
    {
        private readonly Half_Chess__Razor_Server_.Data.Half_Chess__Razor_Server_Context _context;

        public IndexModel(Half_Chess__Razor_Server_.Data.Half_Chess__Razor_Server_Context context)
        {
            _context = context;
        }

        public IList<TblUsers> TblUsers { get;set; } = default!;

        [BindProperty]
        public new TblUsers User { get; set; } = default!;

        public List<SelectListItem> CountryOptions { get; set; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "USA", Text = "United States" },
            new SelectListItem { Value = "IL", Text = "Israel" },
            new SelectListItem { Value = "GB", Text = "United Kingdom" },
            new SelectListItem { Value = "AU", Text = "Australia" },
            new SelectListItem { Value = "DE", Text = "Germany" }
        };

        public async Task<IActionResult> OnPostAsync()
        {

            if (ModelState.IsValid)
            {
                bool userExists = await _context.TblUsers.AnyAsync(u => u.Id == User.Id);

                if (userExists)
                {
                    ModelState.AddModelError("User.Id", "This ID is already in use.");
                    return Page();
                }

                User.CreatedAt = DateTime.Now;
                _context.TblUsers.Add(User);
                await _context.SaveChangesAsync(); 

                // return RedirectToPage("Success"); 
            }
            return Page();
        }
    
        public async Task OnGetAsync()
            {
                if (_context.TblUsers != null)
                {
                    TblUsers = await _context.TblUsers.ToListAsync();
                }
            }
    }
}
