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
            this.signIn_button = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Turn_comboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // WelcomeLabel
            // 
            this.WelcomeLabel.AutoSize = true;
            this.WelcomeLabel.Font = new System.Drawing.Font("Rockwell Nova Cond", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.label2.Location = new System.Drawing.Point(435, 246);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(219, 108);
            this.label2.TabIndex = 1;
            this.label2.Text = "Sign In";
            // 
            // signIn_textBox
            // 
            this.signIn_textBox.Location = new System.Drawing.Point(444, 374);
            this.signIn_textBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.signIn_textBox.Name = "signIn_textBox";
            this.signIn_textBox.Size = new System.Drawing.Size(211, 22);
            this.signIn_textBox.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Rockwell Nova Cond", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(316, 365);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 47);
            this.label3.TabIndex = 4;
            this.label3.Text = "Player ID";
            this.toolTip1.SetToolTip(this.label3, "Enter the ID you registered with");
            // 
            // signIn_button
            // 
            this.signIn_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.signIn_button.Font = new System.Drawing.Font("Rockwell Nova Cond", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.signIn_button.Location = new System.Drawing.Point(456, 430);
            this.signIn_button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.signIn_button.Name = "signIn_button";
            this.signIn_button.Size = new System.Drawing.Size(179, 65);
            this.signIn_button.TabIndex = 5;
            this.signIn_button.Text = "Let\'s Go!";
            this.signIn_button.UseVisualStyleBackColor = true;
            this.signIn_button.Click += new System.EventHandler(this.signIn_button_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Rockwell Nova", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(19, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(817, 96);
            this.label1.TabIndex = 6;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // Turn_comboBox
            // 
            this.Turn_comboBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Turn_comboBox.FormattingEnabled = true;
            this.Turn_comboBox.Items.AddRange(new object[] {
            "10",
            "20 ",
            "30",
            "40 ",
            "50",
            "60"});
            this.Turn_comboBox.Location = new System.Drawing.Point(823, 372);
            this.Turn_comboBox.Name = "Turn_comboBox";
            this.Turn_comboBox.Size = new System.Drawing.Size(140, 24);
            this.Turn_comboBox.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Rockwell Nova Cond", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(817, 333);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(161, 31);
            this.label4.TabIndex = 8;
            this.label4.Text = "Turn time in seconds";
            // 
            // LoginForm
            // 
            this.AcceptButton = this.signIn_button;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1045, 690);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Turn_comboBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.signIn_button);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.signIn_textBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.WelcomeLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "LoginForm";
            this.Text = "Half Chess  ";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label WelcomeLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox signIn_textBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button signIn_button;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ComboBox Turn_comboBox;
        private System.Windows.Forms.Label label4;
    }
}

