namespace UIWindows
{
    public partial class FormGameSettings
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
            this.buttonDone = new System.Windows.Forms.Button();
            this.radioButtonSmallBoardSize = new System.Windows.Forms.RadioButton();
            this.radioButtonMediumBoardSize = new System.Windows.Forms.RadioButton();
            this.radioButtonLargeBoardSize = new System.Windows.Forms.RadioButton();
            this.labelBoardSize = new System.Windows.Forms.Label();
            this.textBoxPlayer1Name = new System.Windows.Forms.TextBox();
            this.textBoxPlayer2Name = new System.Windows.Forms.TextBox();
            this.labelPlayers = new System.Windows.Forms.Label();
            this.labelPlayer1 = new System.Windows.Forms.Label();
            this.checkBoxPlayer2 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // buttonDone
            // 
            this.buttonDone.Location = new System.Drawing.Point(140, 164);
            this.buttonDone.Name = "buttonDone";
            this.buttonDone.Size = new System.Drawing.Size(75, 23);
            this.buttonDone.TabIndex = 0;
            this.buttonDone.Text = "Done";
            this.buttonDone.UseVisualStyleBackColor = true;
            this.buttonDone.Click += new System.EventHandler(this.buttonDone_Click);
            // 
            // radioButtonSmallBoardSize
            // 
            this.radioButtonSmallBoardSize.AutoSize = true;
            this.radioButtonSmallBoardSize.Checked = true;
            this.radioButtonSmallBoardSize.Location = new System.Drawing.Point(32, 41);
            this.radioButtonSmallBoardSize.Name = "radioButtonSmallBoardSize";
            this.radioButtonSmallBoardSize.Size = new System.Drawing.Size(48, 17);
            this.radioButtonSmallBoardSize.TabIndex = 1;
            this.radioButtonSmallBoardSize.TabStop = true;
            this.radioButtonSmallBoardSize.Text = "6 x 6";
            this.radioButtonSmallBoardSize.UseVisualStyleBackColor = true;
            // 
            // radioButtonMediumBoardSize
            // 
            this.radioButtonMediumBoardSize.AutoSize = true;
            this.radioButtonMediumBoardSize.Location = new System.Drawing.Point(87, 41);
            this.radioButtonMediumBoardSize.Name = "radioButtonMediumBoardSize";
            this.radioButtonMediumBoardSize.Size = new System.Drawing.Size(48, 17);
            this.radioButtonMediumBoardSize.TabIndex = 1;
            this.radioButtonMediumBoardSize.Text = "8 x 8";
            this.radioButtonMediumBoardSize.UseVisualStyleBackColor = true;
            // 
            // radioButtonLargeBoardSize
            // 
            this.radioButtonLargeBoardSize.AutoSize = true;
            this.radioButtonLargeBoardSize.Location = new System.Drawing.Point(141, 41);
            this.radioButtonLargeBoardSize.Name = "radioButtonLargeBoardSize";
            this.radioButtonLargeBoardSize.Size = new System.Drawing.Size(60, 17);
            this.radioButtonLargeBoardSize.TabIndex = 1;
            this.radioButtonLargeBoardSize.Text = "10 x 10";
            this.radioButtonLargeBoardSize.UseVisualStyleBackColor = true;
            // 
            // labelBoardSize
            // 
            this.labelBoardSize.AutoSize = true;
            this.labelBoardSize.Location = new System.Drawing.Point(19, 9);
            this.labelBoardSize.Name = "labelBoardSize";
            this.labelBoardSize.Size = new System.Drawing.Size(61, 13);
            this.labelBoardSize.TabIndex = 2;
            this.labelBoardSize.Text = "Board Size:";
            // 
            // textBoxPlayer1Name
            // 
            this.textBoxPlayer1Name.Location = new System.Drawing.Point(99, 95);
            this.textBoxPlayer1Name.Name = "textBoxPlayer1Name";
            this.textBoxPlayer1Name.Size = new System.Drawing.Size(116, 20);
            this.textBoxPlayer1Name.TabIndex = 3;
            // 
            // textBoxPlayer2Name
            // 
            this.textBoxPlayer2Name.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxPlayer2Name.Enabled = false;
            this.textBoxPlayer2Name.Location = new System.Drawing.Point(99, 121);
            this.textBoxPlayer2Name.Name = "textBoxPlayer2Name";
            this.textBoxPlayer2Name.Size = new System.Drawing.Size(116, 20);
            this.textBoxPlayer2Name.TabIndex = 3;
            this.textBoxPlayer2Name.Text = "[Computer]";
            // 
            // labelPlayers
            // 
            this.labelPlayers.AutoSize = true;
            this.labelPlayers.Location = new System.Drawing.Point(19, 72);
            this.labelPlayers.Name = "labelPlayers";
            this.labelPlayers.Size = new System.Drawing.Size(44, 13);
            this.labelPlayers.TabIndex = 2;
            this.labelPlayers.Text = "Players:";
            // 
            // labelPlayer1
            // 
            this.labelPlayer1.AutoSize = true;
            this.labelPlayer1.Location = new System.Drawing.Point(19, 98);
            this.labelPlayer1.Name = "labelPlayer1";
            this.labelPlayer1.Size = new System.Drawing.Size(48, 13);
            this.labelPlayer1.TabIndex = 4;
            this.labelPlayer1.Text = "Player 1:";
            // 
            // checkBoxPlayer2
            // 
            this.checkBoxPlayer2.AutoSize = true;
            this.checkBoxPlayer2.Location = new System.Drawing.Point(22, 123);
            this.checkBoxPlayer2.Name = "checkBoxPlayer2";
            this.checkBoxPlayer2.Size = new System.Drawing.Size(67, 17);
            this.checkBoxPlayer2.TabIndex = 5;
            this.checkBoxPlayer2.Text = "Player 2:";
            this.checkBoxPlayer2.UseVisualStyleBackColor = true;
            this.checkBoxPlayer2.CheckedChanged += new System.EventHandler(this.checkBoxPlayer2_CheckedChanged);
            // 
            // FormGameSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(231, 200);
            this.Controls.Add(this.checkBoxPlayer2);
            this.Controls.Add(this.labelPlayer1);
            this.Controls.Add(this.textBoxPlayer2Name);
            this.Controls.Add(this.textBoxPlayer1Name);
            this.Controls.Add(this.labelPlayers);
            this.Controls.Add(this.labelBoardSize);
            this.Controls.Add(this.radioButtonLargeBoardSize);
            this.Controls.Add(this.radioButtonMediumBoardSize);
            this.Controls.Add(this.radioButtonSmallBoardSize);
            this.Controls.Add(this.buttonDone);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormGameSettings";
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Game Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonDone;
        private System.Windows.Forms.RadioButton radioButtonSmallBoardSize;
        private System.Windows.Forms.RadioButton radioButtonMediumBoardSize;
        private System.Windows.Forms.RadioButton radioButtonLargeBoardSize;
        private System.Windows.Forms.Label labelBoardSize;
        private System.Windows.Forms.TextBox textBoxPlayer1Name;
        private System.Windows.Forms.TextBox textBoxPlayer2Name;
        private System.Windows.Forms.Label labelPlayers;
        private System.Windows.Forms.Label labelPlayer1;
        private System.Windows.Forms.CheckBox checkBoxPlayer2;
    }
}