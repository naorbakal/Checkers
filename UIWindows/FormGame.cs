using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UIWindows
{
    public partial class FormGame : Form
    {
        public enum ePlayersNumber
        {
            One = 1,
            Two,
        }

        private const int k_PictureBoxWidth = 50;
        private const int k_PictureBoxHeight = 50;
        private const int k_StartingPictureBoxX = 0;
        private const int k_StartingPictureBoxY = 40;
        private const int k_WidthExtention = 20;
        private const int k_HeightExtention = 80;

        public event EventHandler GameDetailsFilled;

        public event EventHandler MoveEnterd;

        public event EventHandler MessageBoxAnswered;

        private readonly FormGameSettings r_FormGameSettings = new FormGameSettings();
        private PictureBoxGameTool[,] pictureBoxMatrix;
        private PictureBoxGameTool pictureBoxPressed;
        private Label labelPlayer1 = new Label();
        private Label labelPlayer2 = new Label();
        private bool anotherPictureBoxPressed = false;

        public FormGame()
        {
            InitializeComponent();
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            PictureBoxGameTool currentGameToolPressed = sender as PictureBoxGameTool;
            MoveEnterdEventArgs me;
            if (currentGameToolPressed.IsEnabled)
            {
                if (anotherPictureBoxPressed)
                {
                    if (currentGameToolPressed.Image == null)
                    {
                        me = createMoveEnterdEventArgs(pictureBoxPressed.PlaceOnBoard, currentGameToolPressed.PlaceOnBoard);
                        OnMoveEnterd(me);
                        anotherPictureBoxPressed = false;
                        pictureBoxPressed.BorderStyle = BorderStyle.FixedSingle;
                        pictureBoxPressed.IsEnabled = true;
                        pictureBoxPressed.BackgroundImage = global::UIWindows.Properties.Resources.EnabledBackground;
                    }
                }
                else
                {
                    if (currentGameToolPressed.Image != null)
                    {
                        pictureBoxPressed = currentGameToolPressed;
                        currentGameToolPressed.BorderStyle = BorderStyle.Fixed3D;
                        currentGameToolPressed.IsEnabled = false;
                        anotherPictureBoxPressed = true;
                        pictureBoxPressed.BackgroundImage = global::UIWindows.Properties.Resources.GameToolPressed;
                    }
                }
            }
            else
            {
                anotherPictureBoxPressed = false;
                currentGameToolPressed.BorderStyle = BorderStyle.FixedSingle;
                currentGameToolPressed.IsEnabled = true;
                pictureBoxPressed.BackgroundImage = global::UIWindows.Properties.Resources.EnabledBackground;
            }
        }

        public void ErrorMessageBox(string i_ErrorMessage)
        {
            MessageBox.Show(i_ErrorMessage);
        }

        public void ResetGameBoard()
        {
            foreach (PictureBoxGameTool currentGameTool in pictureBoxMatrix)
            {
                currentGameTool.BackColor = Color.Black;
                currentGameTool.BackgroundImage = global::UIWindows.Properties.Resources.DisabledBackground;
                currentGameTool.Image = null;
            }
        }

        private MoveEnterdEventArgs createMoveEnterdEventArgs(Point i_From, Point i_To)
        {
            MoveEnterdEventArgs result = new MoveEnterdEventArgs(i_From, i_To);

            return result;
        }

        public void m_FormGameSettings_FormClosed(object sender, FormClosedEventArgs e)
        { 
            if (string.IsNullOrEmpty(r_FormGameSettings.Player1Name))
            {
                r_FormGameSettings.Player1Name = "Player 1";
            }

            if (string.IsNullOrEmpty(r_FormGameSettings.Player2Name))
            {
                r_FormGameSettings.Player2Name = "Player 2";
            }

            if (r_FormGameSettings.IsPlayer2PC)
            {
                r_FormGameSettings.Player2Name = "Computer";
            }

            DetailsFilledEventArgs df = new DetailsFilledEventArgs(
            r_FormGameSettings.Player1Name,
            r_FormGameSettings.Player2Name,
            r_FormGameSettings.BoardSize,
            r_FormGameSettings.IsPlayer2PC);
            pictureBoxMatrix = new PictureBoxGameTool[r_FormGameSettings.BoardSize, r_FormGameSettings.BoardSize];
            setFormBoarders();
            createPictureBoxMatrix();
            setPlayersLabels();
            OnGameDetailsFiled(df);
        }

        public void MarkCurrentPlayerLabel(string i_CurrentPlayerName)
        {
            if(i_CurrentPlayerName == r_FormGameSettings.Player1Name)
            {
                labelPlayer1.ForeColor = Color.Blue;
                labelPlayer2.ForeColor = Color.Black;
            }
            else if(i_CurrentPlayerName == r_FormGameSettings.Player2Name)
            {
                labelPlayer2.ForeColor = Color.Red;
                labelPlayer1.ForeColor = Color.Black;
            }
        }

        private void setPlayersLabels()
        {
            int playerStartingScore = 0;

            setPlayersLabelLocation();
            setPlayerLabelsFont();
            UpdateScoreBoard(playerStartingScore, playerStartingScore);
            this.Controls.Add(labelPlayer1);
            this.Controls.Add(labelPlayer2);
        }

        private void setPlayerLabelsFont()
        {
            labelPlayer1.Font = new Font(labelPlayer1.Font, FontStyle.Bold);
            labelPlayer2.Font = new Font(labelPlayer2.Font, FontStyle.Bold);
        }

        private void setPlayersLabelLocation()
        {
            PictureBoxGameTool middlePictureBox = pictureBoxMatrix[0, (r_FormGameSettings.BoardSize / 2) - 1];
            int gamePictureBoxWidth = middlePictureBox.Width;
            int gamePictureBoxHeight = middlePictureBox.Height / 2;
            Point middle = middlePictureBox.Location;
            Point player1LabelLocation = middle, player2LabelLocation = middle;

            player1LabelLocation.Offset(-middlePictureBox.Width, -gamePictureBoxHeight);
            player2LabelLocation.Offset(middlePictureBox.Width, -gamePictureBoxHeight);
            labelPlayer1.Location = player1LabelLocation;
            labelPlayer1.AutoSize = true;
            labelPlayer2.Location = player2LabelLocation;
            labelPlayer2.AutoSize = true;
        }

        private void updateLabelsText(string i_PlayerName, ePlayersNumber i_PlayerNumber, int i_PlayerScore)
        {
            if (i_PlayerNumber == ePlayersNumber.One)
            {
                labelPlayer1.Text = string.Format("{0}: {1}", i_PlayerName, i_PlayerScore);
            }
            else
            {
                labelPlayer2.Text = string.Format("{0}: {1}", i_PlayerName, i_PlayerScore);
            }
        }

        public void UpdateScoreBoard(int i_Player1Score, int i_Player2Score)
        {
            labelPlayer1.Text = string.Format("{0}: {1}", r_FormGameSettings.Player1Name, i_Player1Score);
            labelPlayer2.Text = string.Format("{0}: {1}", r_FormGameSettings.Player2Name, i_Player2Score);
        }

        private void setFormBoarders()
        {
            this.Size = new Size((r_FormGameSettings.BoardSize * k_PictureBoxWidth) + k_WidthExtention, (r_FormGameSettings.BoardSize * k_PictureBoxHeight) + k_HeightExtention);
        }

        private void createPictureBoxMatrix()
        {
            bool newLine = false, isFirstPictureBox = true;
            PictureBox lastPictureBoxInMatrix = new PictureBox();

            for (int i = 0; i < r_FormGameSettings.BoardSize; i++)
            {
                for (int j = 0; j < r_FormGameSettings.BoardSize; j++)
                {
                    PictureBoxGameTool currentPictureBox = new PictureBoxGameTool(i, j);
                    setPictureBoxLocation(currentPictureBox, newLine, isFirstPictureBox, lastPictureBoxInMatrix);
                    intializePictureBox(currentPictureBox);
                    pictureBoxMatrix[i, j] = currentPictureBox;
                    this.Controls.Add(currentPictureBox);
                    newLine = false;
                    isFirstPictureBox = false;
                    lastPictureBoxInMatrix = currentPictureBox;
                }

                newLine = true;
            }
        }

        private void intializePictureBox(PictureBoxGameTool i_CurrentPictureBox)
        {
            setPictureBoxFigure(i_CurrentPictureBox);
            i_CurrentPictureBox.Enabled = false;
            i_CurrentPictureBox.Click += pictureBox_Click;
        }

        private void setPictureBoxFigure(PictureBoxGameTool i_CurrentPictureBox)
        {
            i_CurrentPictureBox.Height = k_PictureBoxHeight;
            i_CurrentPictureBox.Width = k_PictureBoxWidth;
        }

        private void setPictureBoxLocation(
            PictureBoxGameTool i_CurrentPictureBox,
            bool i_NewLine,
            bool i_IsFirstPictureBox,
            PictureBox i_LastPictureBoxInMatrix)
        {
            Point newLocation;
            int   pictureBoxMatrixMaxLine = r_FormGameSettings.BoardSize - 1;
            int   pictureBoxMatrixMaxCol = r_FormGameSettings.BoardSize - 1;

            if (i_IsFirstPictureBox)
            {
                newLocation = new Point(k_StartingPictureBoxX, k_StartingPictureBoxY);
            }
            else
            {
                newLocation = i_LastPictureBoxInMatrix.Location;
                if (!i_NewLine)
                {
                    newLocation.Offset(i_LastPictureBoxInMatrix.Width, 0);
                }
                else
                {
                    newLocation.X = k_StartingPictureBoxX;
                    newLocation.Offset(0, i_LastPictureBoxInMatrix.Height);
                }
            }

            i_CurrentPictureBox.Location = newLocation;         
        }

        protected virtual void OnGameDetailsFiled(DetailsFilledEventArgs df)
        {
            if (GameDetailsFilled != null)
            {
                GameDetailsFilled(this, df);
            }
        }

        protected virtual void OnMoveEnterd(MoveEnterdEventArgs me)
        {
            if (MoveEnterd != null)
            {
                MoveEnterd(this, me);
            }
        }

        public void AddGameToolsToGameBoard(List<Point> i_PointsToAdd, Image i_GameToolImage)
        {
            foreach (Point currentPoint in i_PointsToAdd)
            {
                setPictureBoxImage(currentPoint, i_GameToolImage);
            }

            EnableGamePictureBoxes(i_PointsToAdd);
        }

        private void setPictureBoxImage(Point i_CurrentPoint, Image i_GameToolImage)
        {
            pictureBoxMatrix[i_CurrentPoint.X, i_CurrentPoint.Y].Image = i_GameToolImage;
            pictureBoxMatrix[i_CurrentPoint.X, i_CurrentPoint.Y].SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
        }

        public void EraseGameToolsFromGameBoard(List<Point> i_PointsToErase)
        {
            foreach (Point currentPoint in i_PointsToErase)
            {
                pictureBoxMatrix[currentPoint.X, currentPoint.Y].Image = null;
                pictureBoxMatrix[currentPoint.X, currentPoint.Y].BorderStyle = BorderStyle.FixedSingle;
            }
        }
        
        public void EnableGamePictureBoxes(List<Point> i_GamePictureBoxesToEnable)
        {
            foreach (Point currentPoint in i_GamePictureBoxesToEnable)
            {                
                pictureBoxMatrix[currentPoint.X, currentPoint.Y].Enabled = true;
                pictureBoxMatrix[currentPoint.X, currentPoint.Y].BackgroundImage = global::UIWindows.Properties.Resources.EnabledBackground;
                pictureBoxMatrix[currentPoint.X, currentPoint.Y].IsEnabled = true;
            }
        }

        public void DisableGamePictureBoxes(List<Point> i_GamePictureBoxesToDisable)
        {
            foreach (Point currentPoint in i_GamePictureBoxesToDisable)
            {
                pictureBoxMatrix[currentPoint.X, currentPoint.Y].Enabled = false;
                pictureBoxMatrix[currentPoint.X, currentPoint.Y].IsEnabled = false;
            }
        }

        public void CreateYesNoMessageBox(string i_MessageBoxString, string i_Caption)
        {
            DialogResult dialogResult = MessageBox.Show(i_MessageBoxString, i_Caption, MessageBoxButtons.YesNo);
            MessageBoxAnsweredEventArgs mba;
            bool isMessageBoxAnswerIsYes = false;

            if (dialogResult == DialogResult.Yes)
            {
                isMessageBoxAnswerIsYes = true;
            }
            else if (dialogResult == DialogResult.No)
            {
                isMessageBoxAnswerIsYes = false;
            }

            mba = new MessageBoxAnsweredEventArgs(isMessageBoxAnswerIsYes);
            OnMessageBoxAnswered(mba);
        }

        private void OnMessageBoxAnswered(MessageBoxAnsweredEventArgs mba)
        {
            if (MessageBoxAnswered != null)
            {
                MessageBoxAnswered(this, mba);
            }
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;  
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Text = "Damka";
            this.BackColor = Color.Silver;
            this.Load += FormGame_Load;
        }

        private void FormGame_Load(object sender, EventArgs e)
        {
            r_FormGameSettings.FormClosed += m_FormGameSettings_FormClosed;
            r_FormGameSettings.ShowDialog();
        }

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

        #endregion
    }
}
