using Half_Chess__Winform_Client_.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        static HttpClient client = new HttpClient();
        private const string PATH = "https://localhost:44382/";

        public LoginForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            client.BaseAddress = new Uri(PATH);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
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

        private async void signIn_button_Click(object sender, EventArgs e)
        {
            string playerId = signIn_textBox.Text;
            if (playerId.Length == 0)
                return;

            string toUser = "api/TblUsers/" + signIn_textBox.Text;
            // MessageBox.Show(PATH + toUser);
            User user = await GetUserAsync(PATH + toUser);
            if (user != null)
                WelcomeLabel.Text = user.FirstName;
        }
    }
}
