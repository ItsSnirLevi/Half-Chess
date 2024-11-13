using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Half_Chess__Winform_Client_
{
    public partial class DatabaseForm : Form
    {
        private GameDBEntities db = new GameDBEntities();

        public DatabaseForm()
        {
            InitializeComponent();
        }

        private void DatabaseForm_Load(object sender, EventArgs e)
        {
            tblBindingSource.DataSource = db.TblGames.ToList();
            tblDataGridView.DataSource = tblBindingSource;
            tblBindingNavigator.BindingSource = tblBindingSource;
        }

        public async Task DeleteGameAsync(int gameId)
        {
            // Find the game by ID
            var game = await db.TblGames.FindAsync(gameId);

            if (game != null)
            {
                // Remove the game from the context
                db.TblGames.Remove(game);

                // Save changes to the database
                await db.SaveChangesAsync();
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            int gameId = Convert.ToInt32(textBox1.Text); // Replace with your method to get the selected game's ID

            if (gameId > 0) // Ensure a valid ID
            {
                await DeleteGameAsync(gameId);
                MessageBox.Show("Game deleted successfully.");
            }
            else
            {
                MessageBox.Show("Please select a valid game to delete.");
            }

            tblDataGridView.DataSource = tblBindingSource;
        }
    }
}
