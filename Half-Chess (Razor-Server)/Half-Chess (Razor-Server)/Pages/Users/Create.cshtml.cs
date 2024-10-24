using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Half_Chess__Razor_Server_.Data;
using Half_Chess__Razor_Server_.Models;

namespace Half_Chess__Razor_Server_.Pages.Users
{
    public class CreateModel : PageModel
    {
        private readonly Half_Chess__Razor_Server_.Data.Half_Chess__Razor_Server_Context _context;

        public CreateModel(Half_Chess__Razor_Server_.Data.Half_Chess__Razor_Server_Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public TblUsers TblUsers { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.TblUsers == null || TblUsers == null)
            {
                return Page();
            }

            _context.TblUsers.Add(TblUsers);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
