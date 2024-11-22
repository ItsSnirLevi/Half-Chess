using Half_Chess__Winform_Client_.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Half_Chess__Winform_Client_
{
    public partial class LoginForm : Form
    {
        private static readonly HttpClient client = new HttpClient();
        private const string PATH = "https://localhost:44382/";

        public User user = null;
        public int turnTime;
        public bool isWhite = true;

        public GameDBEntities db = new GameDBEntities();

        public LoginForm()
        {
            InitializeComponent();
            this.Size = new Size(800, 600); 
            this.MinimumSize = new Size(800, 600); 
            this.MaximumSize = new Size(800, 600);
            Turn_comboBox.SelectedIndex = 1;
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            client.BaseAddress = new Uri(PATH);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            tblBindingSource.DataSource = db.TblGames.ToList();
            // tblDataGridView.DataSource = tblBindingSource;
        }

        async Task<User> GetUserAsync(string path)
        {
            User user = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                user = await response.Content.ReadAsAsync<User>();
            }
            return user;
        }

        async Task<bool> UpdateLastPlayedAsync(string path, int userId, DateTime? lastPlayed)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(path + $"api/TblUsers/{userId}/lastplayed", lastPlayed);
            return response.IsSuccessStatusCode;
        }

        private async void submit_button_Click(object sender, EventArgs e)
        {
            string playerId = signIn_textBox.Text;
            if (playerId.Length == 0)
                return;

            string toUser = "api/TblUsers/" + signIn_textBox.Text;
            user = await GetUserAsync(PATH + toUser);

            if (user == null)
            {
                MessageBox.Show("Please register at our website before trying to sign in!", "User ID Does Not Exist",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
            else
            {
                Player_label.Text = "Welcome, " + user.FirstName;
                // await GetGameDB();
                await GetUserGames(user.Id);
                tblBindingSource.DataSource =
                        (from game in db.TblGames
                         // where game.PlayerID == user.Id
                         select new
                         {
                             GameID = game.Id,
                             GameTime = game.StartGameTime,
                             Duration = game.GameDuration,
                             Winner = game.Winner
                         }).ToList();
                tblDataGridView.DataSource = tblBindingSource;
            }
        }

        //private async Task GetGameDB()
        //{
        //    string gamesApiUrl = "api/TblUsers/GetGames";
        //    var games = await GetGamesAsync(PATH + gamesApiUrl);

        //    if (games != null)
        //    {
        //        // Reset TblGames: Remove existing records
        //        db.TblGames.RemoveRange(db.TblGames);
        //        await db.SaveChangesAsync();

        //        foreach (var game in games)
        //        {
        //            db.TblGames.Add(new TblGames
        //            {        
        //                Id = game.Id,
        //                PlayerID = game.PlayerID,
        //                PlayerName = game.PlayerName,
        //                StartGameTime = game.StartGameTime,
        //                GameDuration = game.GameDuration,
        //                GameMoves = game.GameMoves,
        //                IsWhite = game.IsWhite,
        //                Winner = game.Winner
        //            }) ;
        //        }

        //        await db.SaveChangesAsync();
        //    }
        //}

        private async Task GetUserGames(int playerId)
        {
            string gamesApiUrl = $"api/TblUsers/GetGamesByPlayer/{playerId}";
            var games = await GetGamesAsync(PATH + gamesApiUrl);

            if (games != null)
            {
                // Reset TblGames: Remove existing records
                db.TblGames.RemoveRange(db.TblGames);
                await db.SaveChangesAsync();

                foreach (var game in games)
                {
                    db.TblGames.Add(new TblGames
                    {
                        Id = game.Id,
                        PlayerID = game.PlayerID,
                        PlayerName = game.PlayerName,
                        StartGameTime = game.StartGameTime,
                        GameDuration = game.GameDuration,
                        GameMoves = game.GameMoves,
                        IsWhite = game.IsWhite,
                        Winner = game.Winner
                    });
                }

                await db.SaveChangesAsync();
            }
        }

        private async Task<List<TblGames>> GetGamesAsync(string url)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetStringAsync(url);
                return JsonConvert.DeserializeObject<List<TblGames>>(response);
            }
        }

        private async void startGame_button_Click(object sender, EventArgs e)
        {
            if (user != null)
            {
                user.LastPlayed = DateTime.Now;
                await UpdateLastPlayedAsync(PATH, user.Id, user.LastPlayed);
                turnTime = Convert.ToInt32(Turn_comboBox.Text);
                GameForm form = new GameForm(this);
                form.Show();
            }
            else
            {
                MessageBox.Show("Please sign in!", "User ID Does Not Exist",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
        }

        private void WhiteRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            isWhite = true;
        }

        private void BlackRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            isWhite = false;
        }

        private async void refresh_button_Click(object sender, EventArgs e)
        {
            if (user != null)
            {
                // await GetGameDB();
                await GetUserGames(user.Id);
                tblBindingSource.DataSource =
               (from game in db.TblGames
                // where game.PlayerID == user.Id
                select new
                {
                    GameID = game.Id,
                    GameTime = game.StartGameTime,
                    Duration = game.GameDuration,
                    Winner = (string)game.Winner,
                    Pieces = game.IsWhite ? "White" : "Black"
                }).ToList();
                tblDataGridView.DataSource = tblBindingSource;
            }
        }

        private void DB_Button_Click(object sender, EventArgs e)
        {
            
            DatabaseForm form = new DatabaseForm();
            form.Show();
        }

        private void ReplayButton_Click(object sender, EventArgs e)
        {
            if (tblDataGridView.SelectedRows.Count > 0) 
            {
                var selectedRow = tblDataGridView.SelectedRows[0];
                var id = Convert.ToInt32(selectedRow.Cells[0].Value);

                TblGames row = db.TblGames.FirstOrDefault(r => r.Id == id);


                ReplayForm form = new ReplayForm(row);
                form.Show();

                // MessageBox.Show("Selected ID: " + id);
            }

        }
    }
}
