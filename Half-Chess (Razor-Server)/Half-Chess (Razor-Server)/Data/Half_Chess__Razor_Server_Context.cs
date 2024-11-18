using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Half_Chess__Razor_Server_.Models;

namespace Half_Chess__Razor_Server_.Data
{
    public class Half_Chess__Razor_Server_Context : DbContext
    {
        public Half_Chess__Razor_Server_Context (DbContextOptions<Half_Chess__Razor_Server_Context> options)
            : base(options)
        {
        }

        public DbSet<Half_Chess__Razor_Server_.Models.TblUsers> TblUsers { get; set; } = default!;
        public DbSet<Half_Chess__Razor_Server_.Models.TblGames> TblGames { get; set; } = default!;

    }
}
