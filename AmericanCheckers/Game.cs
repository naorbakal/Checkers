using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace AmericanCheckers
{
    public class Game
    {
        public enum eGameResult
        {
            Draw,
            Player1Win,
            Player2Win,
        }

        // $G$ SFN-012 (+15) Bonus: Events in the Logic layer are handled by the UI.
        public EventHandler NewGameStarted;
        public EventHandler BoardUpdated;
        public EventHandler GameEnded;
        private Board m_GameBoard;
        private Player m_CurrentPlayer = new Player();
        private Player m_NextPlayer = new Player();
        private List<Move> m_PlayerRegularMoves = new List<Move>();
        private List<Move> m_PlayerSkipMoves = new List<Move>();
        private GameTool m_LastGameToolPlaced;
        private bool m_NeedToSkip = false;

        public Player CurrentPlayer
        {
            get
            {
                return m_CurrentPlayer;
            }
        }

        public Player NextPlayer
        {
            get
            {
                return m_NextPlayer;
            }
        }

        public Board Board
        {
            get
            {
                return m_GameBoard;
            }
        }

        public GameTool LastGameToolPlaced
        {
            get
            {
                return m_LastGameToolPlaced;
            }
        }

        public bool IsEndGame()
        {
            bool result = false;

            if (m_CurrentPlayer.PlayerTools.Count == 0)
            {
                result = true;
            }
            else
            {
                createPossibleMovesForCurrentPlayer();
                result = m_CurrentPlayer.IfNoLegalMoveLeft(m_PlayerRegularMoves, m_PlayerSkipMoves);
            }

            return result;
        }

        public eGameResult EndGame(out string o_EndGamemessage)
        {
            StringBuilder endGamemessage = new StringBuilder();

            List<Move> nextPlayerSkipMoves, nextPlayerRegularMoves;
            int winnerScore = 0;
            eGameResult gameResult;

            nextPlayerSkipMoves = m_NextPlayer.CreateSkipsArray(m_GameBoard);
            nextPlayerRegularMoves = m_NextPlayer.CreateRegularArray(m_GameBoard);

            if (m_NextPlayer.IfNoLegalMoveLeft(nextPlayerRegularMoves, nextPlayerSkipMoves))
            {
                gameResult = eGameResult.Draw;
                endGamemessage.Append("Tie!");
            }
            else
            {
                gameResult = m_CurrentPlayer.ToolShape == (char)GameTool.eSigns.PlayerO ? eGameResult.Player1Win : eGameResult.Player2Win;
                winnerScore = m_NextPlayer.CalculateWinnerScore(m_CurrentPlayer);
                endGamemessage.Append(string.Format("{0} Won!", m_NextPlayer.Name));
            }

            endGamemessage.Append(string.Format("{0}Another Round?", Environment.NewLine));
            o_EndGamemessage = endGamemessage.ToString();
            m_NextPlayer.Score += winnerScore;

            return gameResult;
        }

        public void ResetGame()
        {
            m_GameBoard.Clear();
            if (m_CurrentPlayer.ToolShape == (char)GameTool.eSigns.PlayerX)
            {
                switchPlayers();
            }

            m_CurrentPlayer.ResetTools(m_GameBoard);
            m_NextPlayer.ResetTools(m_GameBoard);
        }

        private void updatePlayersDetails(string i_Player1Name, string i_Player2Name, bool i_IsPc)
        {
            int player2Type = i_IsPc == true ? (int)Player.ePLayerType.Computer : (int)Player.ePLayerType.Human;

            m_CurrentPlayer.Name = i_Player1Name;
            m_NextPlayer.Name = i_Player2Name;
            m_CurrentPlayer.InitializePlayer((int)Player.ePLayerType.Human, (int)Player.ePlayerNumber.PlayerOne, m_GameBoard);
            m_NextPlayer.InitializePlayer(player2Type, (int)Player.ePlayerNumber.PlayerTwo, m_GameBoard);
        }

        private void updateBoardSize(int i_BoardSize)
        {
            m_GameBoard = new Board(i_BoardSize, i_BoardSize);
        }

        public bool ImplementMove(Move i_InputMove, out string o_Errormessage)
        {
            BoardUpdatedEventArgs bu;
            GameEndedEventArgs    ge;
            Move                  pcMove = new Move();
            bool                  result = false, isSkipMove = false;              
            string                endGameMessage;

            createPossibleMovesForCurrentPlayer();
            isSkipMove = m_PlayerSkipMoves.Count != 0;

            if (isLegalMove(ref i_InputMove, out o_Errormessage))
            {
                m_LastGameToolPlaced = i_InputMove.Execute(m_GameBoard, m_CurrentPlayer.PlayerTools, m_NextPlayer.PlayerTools);

                m_PlayerSkipMoves.Clear();
                m_LastGameToolPlaced.AddSkipMoves(m_GameBoard, m_PlayerSkipMoves);

                if (isSkipMoveLeft() && isSkipMove)
                {
                    m_NeedToSkip = true;
                }
                else
                {
                    switchPlayers();
                    m_NeedToSkip = false;
                }

                bu = new BoardUpdatedEventArgs(i_InputMove);
                OnBoardUpdated(bu);
                result = true;
            }

            if (IsEndGame())
            {
                EndGame(out endGameMessage);
                ge = new GameEndedEventArgs(endGameMessage);
                OnGameEnded(ge);
            }
            else if (CurrentPlayer.IsPc())
            {
                createPossibleMovesForCurrentPlayer();
                pcMove = CurrentPlayer.GetPCMove(m_PlayerRegularMoves, m_PlayerSkipMoves);
                ImplementMove(pcMove, out o_Errormessage);
            }

            return result;
        }

        private void createPossibleMovesForCurrentPlayer()
        {
            m_PlayerRegularMoves = m_CurrentPlayer.CreateRegularArray(m_GameBoard);
            if (!m_NeedToSkip)
            {
                m_PlayerSkipMoves = m_CurrentPlayer.CreateSkipsArray(m_GameBoard);
            }
        }

        private bool isSkipMoveLeft()
        {
            return m_PlayerSkipMoves.Count != 0;
        }

        private void switchPlayers()
        {
            Player.Swap(ref m_CurrentPlayer, ref m_NextPlayer);
        }

        private bool isLegalMove(ref Move i_InputMove, out string o_Errormessage)
        {
            bool result = false;

            o_Errormessage = string.Empty;
            if (m_PlayerSkipMoves.Count != 0)
            {
                if (Move.FindMoveInList(ref i_InputMove, m_PlayerSkipMoves))
                {
                    result = true;
                }
                else
                {
                    o_Errormessage = "You must commit a skip move";
                }
            }
            else
            {
                if (Move.FindMoveInList(ref i_InputMove, m_PlayerRegularMoves))
                {
                    result = true;
                }
                else
                {
                    o_Errormessage = "Invalid move, try again";
                }
            }

            return result;
        }

        public void InitializeStartingDetails(int i_BoardSize, string i_Player1Name, string i_Player2Name, bool i_IsPc)
        {
            updateBoardSize(i_BoardSize);
            updatePlayersDetails(i_Player1Name, i_Player2Name, i_IsPc);
            OnNewGameStarted();
        }

        public void OnNewGameStarted()
        {
            EventArgs e = new EventArgs();

            if (NewGameStarted != null)
            {
                NewGameStarted(this, e);
            }
        }

        public void OnBoardUpdated(BoardUpdatedEventArgs bu)
        {
            if (BoardUpdated != null)
            {
                BoardUpdated(this, bu);
            }
        }

        public void OnGameEnded(GameEndedEventArgs ge)
        {
            if (GameEnded != null)
            {
                GameEnded(this, ge);
            }
        }
    }
}
