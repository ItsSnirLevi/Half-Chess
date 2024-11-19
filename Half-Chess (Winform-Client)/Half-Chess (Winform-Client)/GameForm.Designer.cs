namespace Half_Chess__Winform_Client_
{
    partial class GameForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameForm));
            this.IDLabel = new System.Windows.Forms.Label();
            this.TimerLabel = new System.Windows.Forms.Label();
            this.NameLabel = new System.Windows.Forms.Label();
            this.CountryLabel = new System.Windows.Forms.Label();
            this.drawBtn = new System.Windows.Forms.Button();
            this.clearBtn = new System.Windows.Forms.Button();
            this.ForfeitBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // IDLabel
            // 
            this.IDLabel.AutoSize = true;
            this.IDLabel.Font = new System.Drawing.Font("Rockwell Nova Cond", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IDLabel.Location = new System.Drawing.Point(15, 31);
            this.IDLabel.Name = "IDLabel";
            this.IDLabel.Size = new System.Drawing.Size(84, 47);
            this.IDLabel.TabIndex = 0;
            this.IDLabel.Text = "label1";
            // 
            // TimerLabel
            // 
            this.TimerLabel.AutoSize = true;
            this.TimerLabel.Font = new System.Drawing.Font("Rockwell Nova Cond", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimerLabel.Location = new System.Drawing.Point(805, 351);
            this.TimerLabel.Name = "TimerLabel";
            this.TimerLabel.Size = new System.Drawing.Size(147, 80);
            this.TimerLabel.TabIndex = 1;
            this.TimerLabel.Text = "label2";
            // 
            // NameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.Font = new System.Drawing.Font("Rockwell Nova Cond", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NameLabel.Location = new System.Drawing.Point(15, 75);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(84, 47);
            this.NameLabel.TabIndex = 2;
            this.NameLabel.Text = "label2";
            // 
            // CountryLabel
            // 
            this.CountryLabel.AutoSize = true;
            this.CountryLabel.Font = new System.Drawing.Font("Rockwell Nova Cond", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CountryLabel.Location = new System.Drawing.Point(15, 119);
            this.CountryLabel.Name = "CountryLabel";
            this.CountryLabel.Size = new System.Drawing.Size(84, 47);
            this.CountryLabel.TabIndex = 3;
            this.CountryLabel.Text = "label1";
            // 
            // drawBtn
            // 
            this.drawBtn.Font = new System.Drawing.Font("Rockwell Nova Cond", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.drawBtn.Location = new System.Drawing.Point(12, 213);
            this.drawBtn.Name = "drawBtn";
            this.drawBtn.Size = new System.Drawing.Size(100, 36);
            this.drawBtn.TabIndex = 4;
            this.drawBtn.Text = "Sketch";
            this.drawBtn.UseVisualStyleBackColor = true;
            // 
            // clearBtn
            // 
            this.clearBtn.Font = new System.Drawing.Font("Rockwell Nova Cond", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clearBtn.Location = new System.Drawing.Point(12, 264);
            this.clearBtn.Name = "clearBtn";
            this.clearBtn.Size = new System.Drawing.Size(100, 36);
            this.clearBtn.TabIndex = 5;
            this.clearBtn.Text = "Clear";
            this.clearBtn.UseVisualStyleBackColor = true;
            // 
            // ForfeitBtn
            // 
            this.ForfeitBtn.Font = new System.Drawing.Font("Rockwell Nova Cond", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForfeitBtn.Location = new System.Drawing.Point(819, 477);
            this.ForfeitBtn.Name = "ForfeitBtn";
            this.ForfeitBtn.Size = new System.Drawing.Size(100, 36);
            this.ForfeitBtn.TabIndex = 6;
            this.ForfeitBtn.Text = "Forfeit";
            this.ForfeitBtn.UseVisualStyleBackColor = true;
            this.ForfeitBtn.Click += new System.EventHandler(this.ForfeitBtn_Click);
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1045, 783);
            this.Controls.Add(this.ForfeitBtn);
            this.Controls.Add(this.clearBtn);
            this.Controls.Add(this.drawBtn);
            this.Controls.Add(this.CountryLabel);
            this.Controls.Add(this.NameLabel);
            this.Controls.Add(this.TimerLabel);
            this.Controls.Add(this.IDLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "GameForm";
            this.Text = "Half Chess";
            this.Load += new System.EventHandler(this.GameForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label IDLabel;
        private System.Windows.Forms.Label TimerLabel;
        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.Label CountryLabel;
        private System.Windows.Forms.Button drawBtn;
        private System.Windows.Forms.Button clearBtn;
        private System.Windows.Forms.Button ForfeitBtn;
    }
}