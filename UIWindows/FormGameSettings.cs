using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UIWindows
{
    public partial class FormGameSettings : Form
    {
        public FormGameSettings()
        {
            InitializeComponent();
        }

        public Button ButtonDone
        {
            get
            {
                return buttonDone;
            }
        }

        public string Player1Name
        {
            get
            {
                return textBoxPlayer1Name.Text;
            }

            set
            {
                textBoxPlayer1Name.Text = value;
            }
        }

        public string Player2Name
        {
            get
            {
                return textBoxPlayer2Name.Text;
            }

            set
            {
                textBoxPlayer2Name.Text = value;
            }
        }

        public bool IsPlayer2PC
        {
            get
            {
                return !checkBoxPlayer2.Checked;
            }
        }

        public int BoardSize
        {
            get
            {
                if (radioButtonSmallBoardSize.Checked)
                {
                    return (int)GameManagment.eBoardSizeOptions.SmallSize;
                }
                else if (radioButtonMediumBoardSize.Checked)
                {
                    return (int)GameManagment.eBoardSizeOptions.MediumSize;
                }
                else
                {
                    return (int)GameManagment.eBoardSizeOptions.LargeSize;
                }
            }
        }

        private void buttonDone_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxPlayer1Name.Text) || string.IsNullOrEmpty(textBoxPlayer2Name.Text))
            {
                MessageBox.Show("You must fill the players names");
            }
            else
            {
                this.Close();
            }
        }

        private void checkBoxPlayer2_CheckedChanged(object sender, EventArgs e)
        {
            textBoxPlayer2Name.Enabled = textBoxPlayer2Name.Enabled == true ? false : true;

            if (textBoxPlayer2Name.Enabled)
            {
                this.textBoxPlayer2Name.BackColor = Color.White;
                this.textBoxPlayer2Name.Text = string.Empty;
            }
            else
            {
                this.textBoxPlayer2Name.BackColor = System.Drawing.SystemColors.MenuBar;
                this.textBoxPlayer2Name.Text = "[Computer]";
            }
        }
    }
}
