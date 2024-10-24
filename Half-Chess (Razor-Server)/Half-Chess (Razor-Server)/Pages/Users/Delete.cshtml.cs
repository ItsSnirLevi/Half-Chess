using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Half_Chess__Razor_Server_.Data;
using Half_Chess__Razor_Server_.Models;

namespace Half_Chess__Razor_Server_.Pages.Users
{
    public class DeleteModel : PageModel
    {
        private readonly Half_Chess__Razor_Server_.Data.Half_Chess__Razor_Server_Context _context;

        public DeleteModel(Half_Chess__Razor_Server_.Data.Half_Chess__Razor_Server_Context context)
        {
            _context = context;
        }

        [BindProperty]
      public TblUsers TblUsers { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.TblUsers == null)
            {
                return NotFound();
            }

            var tblusers = await _context.TblUsers.FirstOrDefaultAsync(m => m.Id == id);

            if (tblusers == null)
            {
                return NotFound();
            }
            else 
            {
                TblUsers = tblusers;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.TblUsers == null)
            {
                return NotFound();
            }
            var tblusers = await _context.TblUsers.FindAsync(id);

            if (tblusers != null)
            {
                TblUsers = tblusers;
                _context.TblUsers.Remove(TblUsers);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
