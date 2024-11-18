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
        public IList<TblGames> TblGamesList { get; set; } = new List<TblGames>();

        public IList<PlayerLastPlayed> PlayerLastPlayedList = new List<PlayerLastPlayed>();
        public IList<GamesPlayed> GamesPlayedList = new List<GamesPlayed>();
        public List<SelectListItem> PlayerNames { get; set; } = new List<SelectListItem>();
        // public string SelectedPlayerName { get; set; } = string.Empty;
        public IList<PlayersByCountry> PlayersByCountryList = new List<PlayersByCountry>();
        public IList<PlayersByGames> PlayersByGames { get; set; } = new List<PlayersByGames>();


        public TableFormat CurrentTableFormat { get; set; } = TableFormat.FullTable;

        public enum TableFormat
        {
            FullTable,
            LastPlayedOnly,
            GamesPlayed,
            GroupedByCountry,
            ByGamesPlayedDescending,
            ShowGamesTable
        }

        public async Task OnGetAsync()
        {
            if (_context.TblUsers != null)
            { 
                TblUsers = await _context.TblUsers.ToListAsync();

                PopulatePlayerNames();
            }
        }

        public async Task OnPostSortByNameAsync()
        {
            if (_context.TblUsers != null)
            {
                TblUsers = await _context.TblUsers.OrderBy(p => p.FirstName.ToLower()).ToListAsync();
                PopulatePlayerNames();
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
                PopulatePlayerNames();
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
                PopulatePlayerNames();
            }
        }

        public async Task OnPostShowGamesPlayedByEachPlayerAsync()
        {
            if (_context.TblUsers != null && _context.TblGames != null)
            {
                var players = await _context.TblUsers
                    .Select(p => new
                    {
                        Name = p.FirstName,
                        GamesCount = _context.TblGames.Count(g => g.PlayerID == p.Id) // Count games for each player
                    })
                    .ToListAsync();

                GamesPlayedList = players.Select(p => new GamesPlayed
                {
                    Name = p.Name,
                    Games = p.GamesCount // Use the counted games
                }).ToList();

                CurrentTableFormat = TableFormat.GamesPlayed;
                PopulatePlayerNames();
            }
        }

        public async Task OnPostGetGamesByPlayerAsync(string playerName)
        {
            if (!string.IsNullOrEmpty(playerName) && _context.TblUsers != null)
            {
                // Find the player by name (case-insensitive)
                var player = await _context.TblUsers
                    .FirstOrDefaultAsync(p => p.FirstName.ToLower() == playerName.ToLower());

                if (player != null)
                {
                    // Retrieve the games for the found player using their ID
                    TblGamesList = await _context.TblGames
                        .Where(g => g.PlayerID == player.Id)
                        .ToListAsync();

                    CurrentTableFormat = TableFormat.ShowGamesTable;
                    PopulatePlayerNames();
                }
            }
            else
            {
                await OnGetAsync();
            }
        }

        public async Task OnPostGroupPlayersByCountryAsync()
        {
            if (_context.TblUsers != null)
            {
                var groupedPlayers = await _context.TblUsers
                    .GroupBy(p => p.Country)
                    .Select(g => new PlayersByCountry
                    {
                        Country = g.Key,
                        Players = g.ToList()
                    })
                    .OrderBy(g => g.Country)
                    .ToListAsync();

                PlayersByCountryList = groupedPlayers;
                CurrentTableFormat = TableFormat.GroupedByCountry;
                PopulatePlayerNames();
            }
        }

        public async Task OnPostSortByGamesPlayedAsync()
        {
            if (_context.TblUsers != null)
            {
                // Fetch players and their respective game counts directly in the query
                var playersWithGameCounts = await _context.TblUsers
                    .Select(p => new
                    {
                        Player = p,
                        GamesCount = _context.TblGames.Count(g => g.PlayerID == p.Id) // Inline counting logic
                    })
                    .ToListAsync();

                // Group by game count and order by descending game count
                var groupedPlayers = playersWithGameCounts
                    .GroupBy(p => p.GamesCount)
                    .OrderByDescending(g => g.Key)
                    .Select(g => new PlayersByGames
                    {
                        GamesCount = g.Key,
                        Players = g.Select(p => p.Player).ToList() // Extract the player objects
                    })
                    .ToList();

                PlayersByGames = groupedPlayers;
                CurrentTableFormat = TableFormat.ByGamesPlayedDescending;
                PopulatePlayerNames();
            }
        }

        public async Task OnPostShowGamesTableAsync()
        {
            if (_context.TblGames != null)
            {
                TblGamesList = await _context.TblGames
                    .ToListAsync();

                CurrentTableFormat = TableFormat.ShowGamesTable;
                PopulatePlayerNames();
            }
        }

        private void PopulatePlayerNames()
        {
            PlayerNames = _context.TblUsers
                .GroupBy(u => u.FirstName.ToLower())
                .Select(g => g.First().FirstName)
                .OrderBy(name => name)
                .Select(name => new SelectListItem { Value = name, Text = name })
                .ToList();
        }

    }
}
