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
    public partial class EndGameDialog : Form
    {
        public EndGameDialog(string message)
        {
            InitializeComponent();
            this.Size = new Size(280, 480);
            this.MinimumSize = new Size(280, 480);
            this.MaximumSize = new Size(280, 480);

            this.Text = "Game Over";
            this.StartPosition = FormStartPosition.CenterParent;

            MessageLabel.Text = message;
            this.FormClosing += EndGameDialog_FormClosing;
        }

        private void EndGameDialog_Load(object sender, EventArgs e)
        {

        }

        private void NewGameBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;  // New game chosen
            this.Close();
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;  // New game chosen
            this.Close();
        }

        private void EndGameDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.None)
            {
                this.DialogResult = DialogResult.No;
            }
        }
    }
}
