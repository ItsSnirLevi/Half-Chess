using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Half_Chess__Razor_Server_.Data;
using Half_Chess__Razor_Server_.Models;
using static Half_Chess__Razor_Server_.Pages.Users.IndexModel;

namespace Half_Chess__Razor_Server_.Pages.Users
{
    public class DeleteModel : PageModel
    {
        private readonly Half_Chess__Razor_Server_.Data.Half_Chess__Razor_Server_Context _context;

        public DeleteModel(Half_Chess__Razor_Server_.Data.Half_Chess__Razor_Server_Context context)
        {
            _context = context;
        }

        public DeleteFormat CurrentTableFormat { get; set; } = DeleteFormat.User;

        public enum DeleteFormat
        {
            User,
            Game
        }

        [BindProperty]
        public TblUsers TblUsers { get; set; } = default!;
        [BindProperty]
        public TblGames TblGames { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id, bool isUser)
        {
            if (id == null || _context.TblUsers == null)
            {
                return NotFound();
            }

            CurrentTableFormat = isUser ? DeleteFormat.User : DeleteFormat.Game;

            if (isUser)
            {
                var tblusers = await _context.TblUsers.FirstOrDefaultAsync(m => m.Id == id);

                if (tblusers == null)
                {
                    return NotFound();
                }
                else
                {
                    TblUsers = tblusers;
                }
            } else
            {
                var tblgames = await _context.TblGames.FirstOrDefaultAsync(m => m.Id == id);

                if (tblgames == null)
                {
                    return NotFound();
                }
                else
                {
                    TblGames = tblgames;
                }
            }
            
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id, bool isUser)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (isUser)
            {
                // Handle TblUsers deletion
                var tblusers = await _context.TblUsers.FindAsync(id);
                if (tblusers != null)
                {
                    TblUsers = tblusers;
                    _context.TblUsers.Remove(TblUsers);
                    await _context.SaveChangesAsync();
                }
            }
            else
            {
                // Handle TblGames deletion
                var tblgames = await _context.TblGames.FindAsync(id);
                if (tblgames != null)
                {
                    TblGames = tblgames;
                    _context.TblGames.Remove(TblGames);
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
