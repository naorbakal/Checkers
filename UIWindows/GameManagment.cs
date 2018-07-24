using System;
using System.Collections.Generic;
using System.Drawing;
using AmericanCheckers;

namespace UIWindows
{
    public class GameManagment
    {
        public enum eBoardSizeOptions
        {
            SmallSize = Board.eBoradSize.Small,
            MediumSize = Board.eBoradSize.Medium,
            LargeSize = Board.eBoradSize.Large,
        }

        // $G$ DSN-999 (-2) these members should be readonly.
        private FormGame m_FormGame = new FormGame();
        private Game m_AmericanCheckers = new Game();

        public void Run()
        {
            registerToLogicEvents();
            registerToFormEvents();
            m_FormGame.ShowDialog();
        }

        private void registerToLogicEvents()
        {
            m_AmericanCheckers.NewGameStarted += m_AmericanCheckers_NewGameStarted;
            m_AmericanCheckers.BoardUpdated += m_AmericanCheckers_BoardUpdated;
            m_AmericanCheckers.GameEnded += m_AmericanCheckers_GameEnded;
        }

        private void registerToFormEvents()
        {
            m_FormGame.GameDetailsFilled += m_FormGame_GameDetailsFilled;
            m_FormGame.MoveEnterd += m_FormGame_MoveEnterd;
            m_FormGame.MessageBoxAnswered += m_FormGame_MessageBoxAnswered;
        }

        private void m_AmericanCheckers_GameEnded(object sender, EventArgs e)
        {
            GameEndedEventArgs ge = e as GameEndedEventArgs;

            m_FormGame.CreateYesNoMessageBox(ge.Message, "Damka");
        }

        private void m_FormGame_MessageBoxAnswered(object sender, EventArgs e)
        {
            MessageBoxAnsweredEventArgs mba = e as MessageBoxAnsweredEventArgs;

            if (mba.IsAnsweredYes)
            {
                m_AmericanCheckers_NewGameStarted(sender, e);
            }
            else
            {
                m_FormGame.Close();
            }
        }

        private void m_FormGame_MoveEnterd(object sender, EventArgs e)
        {
            MoveEnterdEventArgs me = e as MoveEnterdEventArgs;
            Move                newMove = new Move(me.From, me.To);
            string              errorMessage;
            bool                isLegalMove;

            isLegalMove = m_AmericanCheckers.ImplementMove(newMove, out errorMessage);
            if (!isLegalMove)
            {
                m_FormGame.ErrorMessageBox(errorMessage);
            }
        }

        private void m_AmericanCheckers_BoardUpdated(object sender, EventArgs e)
        {
            BoardUpdatedEventArgs bu = e as BoardUpdatedEventArgs;
            List<Point>           pointsToAddOnBoard = new List<Point>();
            List<Point>           pointsToEraseOnBoard = new List<Point>();
            List<Point>           pointsToEnableOnBoard;
            List<Point>           pointsToDisableOnBoard;
            Image                 playerToolImage;

            pointsToAddOnBoard.Add(bu.LastMove.To);
            pointsToEraseOnBoard.Add(bu.LastMove.From);
            pointsToEnableOnBoard = createPointsListFromPlayerGameTools(m_AmericanCheckers.CurrentPlayer.PlayerTools);
            pointsToDisableOnBoard = createPointsListFromPlayerGameTools(m_AmericanCheckers.NextPlayer.PlayerTools);
            if (bu.LastMove.IsSkipMove)
            {
                pointsToEraseOnBoard.Add(bu.LastMove.Eaten);
                pointsToEnableOnBoard.Add(bu.LastMove.Eaten);
            }

            getImagePlayerTool(out playerToolImage);
            markCurrentPlayer();
            m_FormGame.AddGameToolsToGameBoard(pointsToAddOnBoard, playerToolImage);
            m_FormGame.EraseGameToolsFromGameBoard(pointsToEraseOnBoard);
            m_FormGame.EnableGamePictureBoxes(pointsToEnableOnBoard);
            m_FormGame.DisableGamePictureBoxes(pointsToDisableOnBoard);
        }

        private void markCurrentPlayer()
        {
            m_FormGame.MarkCurrentPlayerLabel(m_AmericanCheckers.CurrentPlayer.Name);
        }

        private void getImagePlayerTool(out Image o_ImagePlayerGameTool)
        {
            char playerToolSign = m_AmericanCheckers.LastGameToolPlaced.Sign;

            if (playerToolSign == (char)GameTool.eSigns.PlayerO)
            {
                o_ImagePlayerGameTool = global::UIWindows.Properties.Resources.Player1Tool;
            }
            else if (playerToolSign == (char)GameTool.eSigns.PlayerOKing)
            {
                o_ImagePlayerGameTool = global::UIWindows.Properties.Resources.Player1Crown;
            }
            else if (playerToolSign == (char)GameTool.eSigns.PlayerX)
            {
                o_ImagePlayerGameTool = global::UIWindows.Properties.Resources.Player2Tool;
            }
            else
            {
                o_ImagePlayerGameTool = global::UIWindows.Properties.Resources.Player2Crown;
            }
        }

        private List<Point> createPointsListFromPlayerGameTools(List<GameTool> i_PlayerTools)
        {
            List<Point> result = new List<Point>();

            foreach (GameTool current in i_PlayerTools)
            {
                result.Add(current.Location);
            }

            return result;
        }

        private void m_FormGame_GameDetailsFilled(object sender, EventArgs e)
        {
            DetailsFilledEventArgs df = e as DetailsFilledEventArgs;

            m_AmericanCheckers.InitializeStartingDetails(df.GameBoardSize, df.Player1Name, df.Player2Name, df.IsPlayer2PC);
        }

        private void m_AmericanCheckers_NewGameStarted(object sender, EventArgs e)
        {
            List<Point> player1GameToolsPoints = new List<Point>();
            List<Point> player2GameToolsPoints = new List<Point>();
            int         player1Score, player2Score;
            Image       player1GameToolImage = global::UIWindows.Properties.Resources.Player1Tool;
            Image       player2GameToolImage = global::UIWindows.Properties.Resources.Player2Tool;

            m_FormGame.ResetGameBoard();
            m_AmericanCheckers.ResetGame();
            player1GameToolsPoints = createPointsListFromPlayerGameTools(m_AmericanCheckers.CurrentPlayer.PlayerTools);
            player2GameToolsPoints = createPointsListFromPlayerGameTools(m_AmericanCheckers.NextPlayer.PlayerTools);
            m_FormGame.AddGameToolsToGameBoard(player1GameToolsPoints, player1GameToolImage);
            m_FormGame.AddGameToolsToGameBoard(player2GameToolsPoints, player2GameToolImage);
            m_FormGame.DisableGamePictureBoxes(player2GameToolsPoints);
            enabledBufferZone();
            player1Score = m_AmericanCheckers.CurrentPlayer.Score;
            player2Score = m_AmericanCheckers.NextPlayer.Score;
            m_FormGame.UpdateScoreBoard(player1Score, player2Score);
            markCurrentPlayer();
        }

        private void enabledBufferZone()
        {
            List<Point> enabledBufferZonePoints = new List<Point>();
            int startLoopIndex = (m_AmericanCheckers.Board.Height / 2) - 1;
            int endLoopIndex = (m_AmericanCheckers.Board.Height / 2) + 1;

            for (int i = startLoopIndex; i < endLoopIndex; i++)
            {
                for (int j = 0; j < m_AmericanCheckers.Board.Width; j++)
                {
                    if ((i % 2 == 0 && j % 2 != 0) || (i % 2 != 0 && j % 2 == 0))
                    {
                        enabledBufferZonePoints.Add(new Point(i, j));
                    }
                }
            }

            m_FormGame.EnableGamePictureBoxes(enabledBufferZonePoints);
        }
    }
}
