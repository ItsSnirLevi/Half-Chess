namespace Half_Chess__Winform_Client_
{
    partial class LoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.WelcomeLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.signIn_textBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.startGame_button = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Turn_comboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.WhiteRadioButton = new System.Windows.Forms.RadioButton();
            this.BlackRadioButton = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tblDataGridView = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.submit_button = new System.Windows.Forms.Button();
            this.Player_label = new System.Windows.Forms.Label();
            this.refresh_button = new System.Windows.Forms.Button();
            this.tblBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ReplayButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tblDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // WelcomeLabel
            // 
            this.WelcomeLabel.AutoSize = true;
            this.WelcomeLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(62)))), ((int)(((byte)(70)))));
            this.WelcomeLabel.Font = new System.Drawing.Font("Rockwell Nova Cond", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WelcomeLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(173)))), ((int)(((byte)(181)))));
            this.WelcomeLabel.Location = new System.Drawing.Point(12, 9);
            this.WelcomeLabel.Name = "WelcomeLabel";
            this.WelcomeLabel.Size = new System.Drawing.Size(403, 54);
            this.WelcomeLabel.TabIndex = 0;
            this.WelcomeLabel.Text = "Welcome to Half-Chess!";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Rockwell Nova Cond", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(173)))), ((int)(((byte)(181)))));
            this.label2.Location = new System.Drawing.Point(470, 237);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(219, 108);
            this.label2.TabIndex = 1;
            this.label2.Text = "Sign In";
            // 
            // signIn_textBox
            // 
            this.signIn_textBox.Location = new System.Drawing.Point(479, 365);
            this.signIn_textBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.signIn_textBox.Name = "signIn_textBox";
            this.signIn_textBox.Size = new System.Drawing.Size(211, 22);
            this.signIn_textBox.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Rockwell Nova Cond", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.label3.Location = new System.Drawing.Point(355, 354);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 47);
            this.label3.TabIndex = 4;
            this.label3.Text = "Player ID";
            this.toolTip1.SetToolTip(this.label3, "Enter the ID you registered with");
            // 
            // startGame_button
            // 
            this.startGame_button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.startGame_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.startGame_button.Font = new System.Drawing.Font("Rockwell Nova Cond", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startGame_button.Location = new System.Drawing.Point(498, 590);
            this.startGame_button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.startGame_button.Name = "startGame_button";
            this.startGame_button.Size = new System.Drawing.Size(179, 65);
            this.startGame_button.TabIndex = 5;
            this.startGame_button.Text = "Start Game";
            this.startGame_button.UseVisualStyleBackColor = false;
            this.startGame_button.Click += new System.EventHandler(this.startGame_button_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(62)))), ((int)(((byte)(70)))));
            this.label1.Font = new System.Drawing.Font("Rockwell Nova", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.label1.Location = new System.Drawing.Point(12, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(817, 96);
            this.label1.TabIndex = 6;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // Turn_comboBox
            // 
            this.Turn_comboBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Turn_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Turn_comboBox.FormattingEnabled = true;
            this.Turn_comboBox.Items.AddRange(new object[] {
            "10",
            "20 ",
            "30",
            "40 ",
            "50",
            "60"});
            this.Turn_comboBox.Location = new System.Drawing.Point(823, 345);
            this.Turn_comboBox.Name = "Turn_comboBox";
            this.Turn_comboBox.Size = new System.Drawing.Size(140, 24);
            this.Turn_comboBox.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Rockwell Nova Cond", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.label4.Location = new System.Drawing.Point(817, 306);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(161, 31);
            this.label4.TabIndex = 8;
            this.label4.Text = "Turn time in seconds";
            // 
            // WhiteRadioButton
            // 
            this.WhiteRadioButton.AutoSize = true;
            this.WhiteRadioButton.Checked = true;
            this.WhiteRadioButton.Font = new System.Drawing.Font("Rockwell Nova Cond", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WhiteRadioButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.WhiteRadioButton.Location = new System.Drawing.Point(6, 21);
            this.WhiteRadioButton.Name = "WhiteRadioButton";
            this.WhiteRadioButton.Size = new System.Drawing.Size(63, 28);
            this.WhiteRadioButton.TabIndex = 9;
            this.WhiteRadioButton.TabStop = true;
            this.WhiteRadioButton.Text = "White";
            this.WhiteRadioButton.UseVisualStyleBackColor = true;
            this.WhiteRadioButton.CheckedChanged += new System.EventHandler(this.WhiteRadioButton_CheckedChanged);
            // 
            // BlackRadioButton
            // 
            this.BlackRadioButton.AutoSize = true;
            this.BlackRadioButton.Font = new System.Drawing.Font("Rockwell Nova Cond", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BlackRadioButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.BlackRadioButton.Location = new System.Drawing.Point(6, 47);
            this.BlackRadioButton.Name = "BlackRadioButton";
            this.BlackRadioButton.Size = new System.Drawing.Size(63, 28);
            this.BlackRadioButton.TabIndex = 10;
            this.BlackRadioButton.Text = "Black";
            this.BlackRadioButton.UseVisualStyleBackColor = true;
            this.BlackRadioButton.CheckedChanged += new System.EventHandler(this.BlackRadioButton_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Rockwell Nova Cond", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.label5.Location = new System.Drawing.Point(817, 404);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(113, 31);
            this.label5.TabIndex = 11;
            this.label5.Text = "Choose Pieces";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.WhiteRadioButton);
            this.groupBox1.Controls.Add(this.BlackRadioButton);
            this.groupBox1.Location = new System.Drawing.Point(823, 438);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(107, 88);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            // 
            // tblDataGridView
            // 
            this.tblDataGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.tblDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tblDataGridView.Location = new System.Drawing.Point(12, 270);
            this.tblDataGridView.Name = "tblDataGridView";
            this.tblDataGridView.RowHeadersWidth = 51;
            this.tblDataGridView.RowTemplate.Height = 24;
            this.tblDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.tblDataGridView.Size = new System.Drawing.Size(289, 277);
            this.tblDataGridView.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(62)))), ((int)(((byte)(70)))));
            this.label6.Font = new System.Drawing.Font("Rockwell Nova Cond", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(173)))), ((int)(((byte)(181)))));
            this.label6.Location = new System.Drawing.Point(17, 226);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(103, 30);
            this.label6.TabIndex = 15;
            this.label6.Text = "Game History";
            // 
            // submit_button
            // 
            this.submit_button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.submit_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.submit_button.Font = new System.Drawing.Font("Rockwell Nova Cond", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.submit_button.Location = new System.Drawing.Point(513, 410);
            this.submit_button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.submit_button.Name = "submit_button";
            this.submit_button.Size = new System.Drawing.Size(147, 68);
            this.submit_button.TabIndex = 16;
            this.submit_button.Text = "Submit";
            this.submit_button.UseVisualStyleBackColor = false;
            this.submit_button.Click += new System.EventHandler(this.submit_button_Click);
            // 
            // Player_label
            // 
            this.Player_label.AutoSize = true;
            this.Player_label.Font = new System.Drawing.Font("Rockwell Nova Cond", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Player_label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.Player_label.Location = new System.Drawing.Point(489, 524);
            this.Player_label.Name = "Player_label";
            this.Player_label.Size = new System.Drawing.Size(0, 50);
            this.Player_label.TabIndex = 17;
            // 
            // refresh_button
            // 
            this.refresh_button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.refresh_button.Font = new System.Drawing.Font("Rockwell Nova Cond", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.refresh_button.Location = new System.Drawing.Point(226, 227);
            this.refresh_button.Name = "refresh_button";
            this.refresh_button.Size = new System.Drawing.Size(75, 37);
            this.refresh_button.TabIndex = 18;
            this.refresh_button.Text = "Refresh";
            this.refresh_button.UseVisualStyleBackColor = false;
            this.refresh_button.Click += new System.EventHandler(this.refresh_button_Click);
            // 
            // ReplayButton
            // 
            this.ReplayButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.ReplayButton.Font = new System.Drawing.Font("Rockwell Nova Cond", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReplayButton.Location = new System.Drawing.Point(89, 575);
            this.ReplayButton.Name = "ReplayButton";
            this.ReplayButton.Size = new System.Drawing.Size(113, 37);
            this.ReplayButton.TabIndex = 20;
            this.ReplayButton.Text = "Replay Game";
            this.ReplayButton.UseVisualStyleBackColor = false;
            this.ReplayButton.Click += new System.EventHandler(this.ReplayButton_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(62)))), ((int)(((byte)(70)))));
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1045, 189);
            this.panel1.TabIndex = 21;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(62)))), ((int)(((byte)(70)))));
            this.panel2.Location = new System.Drawing.Point(0, 183);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(323, 513);
            this.panel2.TabIndex = 22;
            // 
            // LoginForm
            // 
            this.AcceptButton = this.submit_button;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(40)))), ((int)(((byte)(49)))));
            this.ClientSize = new System.Drawing.Size(1045, 690);
            this.Controls.Add(this.ReplayButton);
            this.Controls.Add(this.refresh_button);
            this.Controls.Add(this.Player_label);
            this.Controls.Add(this.submit_button);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tblDataGridView);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Turn_comboBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.startGame_button);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.signIn_textBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.WelcomeLabel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "LoginForm";
            this.Text = "Half Chess  ";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tblDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label WelcomeLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox signIn_textBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button startGame_button;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ComboBox Turn_comboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton WhiteRadioButton;
        private System.Windows.Forms.RadioButton BlackRadioButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView tblDataGridView;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button submit_button;
        private System.Windows.Forms.BindingSource tblBindingSource;
        private System.Windows.Forms.Label Player_label;
        private System.Windows.Forms.Button refresh_button;
        private System.Windows.Forms.Button ReplayButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}

