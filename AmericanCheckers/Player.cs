using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace AmericanCheckers
{
    public class Player
    {
        private const int k_MaxUserNameSize = 20;

        public enum ePLayerType
        {
            Computer,
            Human,
        }

        public enum ePlayerNumber
        {
            PlayerOne = 1,
            PlayerTwo = 2,
        }

        private int m_PlayerType;
        private string m_Name;
        private char m_ToolShape;
        private string m_LastMovement;
        private int m_Score = 0;
        private List<GameTool> m_PlayerTools = new List<GameTool>();

        public static void Swap(ref Player io_PlayerOne, ref Player io_PlayerTwo)
        {
            Player temp;

            temp = io_PlayerOne;
            io_PlayerOne = io_PlayerTwo;
            io_PlayerTwo = temp;
        }

        public static bool IsValidUserName(string i_UserName)
        {
            bool nameContainSpaces = i_UserName.Contains(" ");

            return i_UserName.Length <= k_MaxUserNameSize && !nameContainSpaces;
        }

        public static bool IsValidPlayerTypeChoise(string i_playerType, out int o_result)
        {
            bool isNumeric = int.TryParse(i_playerType, out o_result);

            return isNumeric && (o_result == (int)Player.ePLayerType.Computer || o_result == (int)Player.ePLayerType.Human);
        }

        public int Score
        {
            get
            {
                return m_Score;
            }

            set
            {
                m_Score = value;
            }
        }

        public int PlayerType
        {
            get
            {
                return m_PlayerType;
            }

            set
            {
                m_PlayerType = value;
            }
        }

        public string Name
        {
            get
            {
                return m_Name;
            }

            set
            {
                m_Name = value;
            }
        }

        public List<GameTool> PlayerTools
        {
            get
            {
                return m_PlayerTools;
            }

            set
            {
                m_PlayerTools = value;
            }
        }

        public char ToolShape
        {
            get
            {
                return m_ToolShape;
            }

            set
            {
                m_ToolShape = value;
            }
        }

        public string LastMovement
        {
            get
            {
                return m_LastMovement;
            }

            set
            {
                m_LastMovement = value;
            }
        }

        public void InitializePlayer(int i_PlayerType, int i_PlayerNumber, Board i_GameBoard)
        {
            m_PlayerType = i_PlayerType;

            m_LastMovement = null;
            m_ToolShape = i_PlayerNumber == (int)ePlayerNumber.PlayerOne ? (char)GameTool.eSigns.PlayerO : (char)GameTool.eSigns.PlayerX;
            i_GameBoard.InitializePlayerTools(this, i_PlayerNumber);
        }

        public bool IsPlayerPC(int i_PlayerType)
        {
            return i_PlayerType == (int)ePLayerType.Computer;
        }

        private bool checkIfKing(GameTool currentPlayerTool)
        {
            return currentPlayerTool.Sign == (char)GameTool.eSigns.PlayerOKing ||
                   currentPlayerTool.Sign == (char)GameTool.eSigns.PlayerXKing;
        }

        public List<Move> CreateRegularArray(Board i_GameBoard)
        {
            List<Move> result = new List<Move>();

            foreach (GameTool value in m_PlayerTools)
            {
                value.AddRegularMoves(i_GameBoard, result);
            }

            return result;
        }

        public List<Move> CreateSkipsArray(Board i_GameBoard)
        {
            List<Move> result = new List<Move>();

            foreach (GameTool value in m_PlayerTools)
            {
                value.AddSkipMoves(i_GameBoard, result);
            }

            return result;
        }

        public int CalculateWinnerScore(Player i_Loser)
        {
            int winnerValueOfTools, loserValueOfTools;

            winnerValueOfTools = getToolsValue();
            loserValueOfTools = i_Loser.getToolsValue();

            return winnerValueOfTools - loserValueOfTools;
        }

        private int getToolsValue()
        {
            int result = 0;

            foreach (GameTool currentGameTool in m_PlayerTools)
            {
                result += currentGameTool.Value;
            }

            return result;
        }

        public bool CanQuit(Player i_Opponent)
        {
            int OpponentToolsValue, currentPlayerToolsValue;

            OpponentToolsValue = i_Opponent.getToolsValue();
            currentPlayerToolsValue = getToolsValue();

            return currentPlayerToolsValue < OpponentToolsValue;
        }

        public void ResetTools(Board i_GameBoard)
        {
            int playerNumber = m_ToolShape == (char)GameTool.eSigns.PlayerO ? (int)ePlayerNumber.PlayerOne : (int)ePlayerNumber.PlayerTwo;

            m_PlayerTools.Clear();
            InitializePlayer(m_PlayerType, playerNumber, i_GameBoard);
        }

        public bool IfNoLegalMoveLeft(List<Move> playerRegularMoves, List<Move> playerSkipMoves)
        {
            return playerRegularMoves.Count == 0 && playerSkipMoves.Count == 0;
        }

        public bool IsPc()
        {
            return m_PlayerType == (int)ePLayerType.Computer;
        }

        public Move GetPCMove(List<Move> playerRegularMoves, List<Move> playerSkipMoves)
        {
            Move   result = new Move();
            Random rnd = new Random();
            int    rndIndextMove;

            if(playerSkipMoves.Count != 0)
            {
                rndIndextMove = rnd.Next(playerSkipMoves.Count);
                result = playerSkipMoves[rndIndextMove];
            }
            else if(playerRegularMoves.Count != 0)
            {
                rndIndextMove = rnd.Next(playerRegularMoves.Count);
                result = playerRegularMoves[rndIndextMove];
            }

            return result;
        }
    }
}
