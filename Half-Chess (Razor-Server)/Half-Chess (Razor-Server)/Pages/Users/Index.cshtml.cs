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
    public class IndexModel : PageModel
    {
        private readonly Half_Chess__Razor_Server_.Data.Half_Chess__Razor_Server_Context _context;

        public IndexModel(Half_Chess__Razor_Server_.Data.Half_Chess__Razor_Server_Context context)
        {
            _context = context;
        }

        public IList<TblUsers> TblUsers { get;set; } = default!;
        public IList<PlayerLastPlayed> PlayerLastPlayedList = new List<PlayerLastPlayed>();
        public TableFormat CurrentTableFormat { get; set; } = TableFormat.FullTable;

        public enum TableFormat
        {
            FullTable,
            LastPlayedOnly,
        }

        public async Task OnGetAsync()
        {
            if (_context.TblUsers != null)
            {
                TblUsers = await _context.TblUsers.ToListAsync();
            }
        }

        public async Task OnPostSortByNameAsync()
        {
            if (_context.TblUsers != null)
            {
                TblUsers = await _context.TblUsers.OrderBy(p => p.FirstName.ToLower()).ToListAsync();
            }
        }

        public async Task OnPostShowLastPlayedAsync()
        {
            if (_context.TblUsers != null)
            {
                var players = await _context.TblUsers
                .Select(p => new
                    {
                        Name = p.FirstName,
                        LastGameDate = p.LastPlayed
                    })
                .OrderByDescending(p => EF.Functions.Collate(p.Name, "Latin1_General_BIN")) 
                .ToListAsync();

                PlayerLastPlayedList = players.Select(p => new PlayerLastPlayed
                    {
                        Name = p.Name,
                        LastGameDate = p.LastGameDate 
                    }).ToList();

                CurrentTableFormat = TableFormat.LastPlayedOnly;
            }
        }

        public async Task OnPostShowFirstPlayersByCountryAsync()
        {
            if (_context.TblUsers != null)
            {
                TblUsers = await _context.TblUsers
                    .Where(p => p.LastPlayed != null)
                    .GroupBy(p => p.Country)
                    .Select(g => g.OrderBy(p => p.LastPlayed).First())
                    .ToListAsync();

                CurrentTableFormat = TableFormat.FullTable; 
            }
        }



    }
}
