using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace AmericanCheckers
{
    public class Board
    { 
        public enum eBoradSize
        {
            Small = 6,
            Medium = 8,
            Large = 10
        }

        private readonly char[,] m_GameBoard;
        private int m_Height;
        private int m_Width;

        public Board(int height, int width)
        {
            m_Height = height;
            m_Width = width;
            m_GameBoard = new char[height, width];
            initializeBufferZone();
        }

        public char this[int i_Row, int i_Colum]
        {
            get
            {               
                return m_GameBoard[i_Row, i_Colum];
            }

            set
            {
                m_GameBoard[i_Row, i_Colum] = value;
            }
        }

        public int Height
        {
            get
            {
                return m_Height;
            }

            set
            {
                m_Height = value;
            }
        }

        public int Width
        {
            get
            {
                return m_Width;
            }

            set
            {
                m_Width = value;
            }
        }

        private void initializeBufferZone()
        {
            int startLoopIndex = (m_Height / 2) - 1;
            int endLoopIndex = (m_Height / 2) + 1;

            for (int i = startLoopIndex; i < endLoopIndex; i++)
            {
                for(int j = 0; j < m_Width; j++)
                {
                    m_GameBoard[i, j] = (char)GameTool.eSigns.Empty;
                }
            }
        }

        public void InitializePlayerTools(Player i_Player, int i_PlayerNumber)
        {
            int startLoopValue, endLoopValue;

            startLoopValue = i_PlayerNumber == (int)Player.ePlayerNumber.PlayerOne ? 0 : (m_Height / 2) + 1;
            endLoopValue = i_PlayerNumber == (int)Player.ePlayerNumber.PlayerOne ? (m_Height / 2) - 1 : m_Height;

            for (int i = startLoopValue; i < endLoopValue; i++)
            {
                for(int j = 0; j < m_Width; j++)
                {
                    if ((i % 2 == 0 && j % 2 != 0) || (i % 2 != 0 && j % 2 == 0))
                    {
                        m_GameBoard[i, j] = i_Player.ToolShape;
                        i_Player.PlayerTools.Add(new GameTool(new Point(i, j), m_GameBoard[i, j]));
                    }
                    else
                    {
                        m_GameBoard[i, j] = (char)GameTool.eSigns.Empty;
                    }
                }
            }
        }

        public void UpdateMoveOnBoard(Move i_Move, GameTool i_GameToolToMove)
        {
            char tempSign = i_GameToolToMove.Sign;

            DeleteFromBoard(i_Move.From);
            if (transferToKing(i_Move.To, i_GameToolToMove))
            {
                i_GameToolToMove.Sign = i_GameToolToMove.FriendSign;
                i_GameToolToMove.FriendSign = tempSign;
            }

            addToBoard(i_Move.To, i_GameToolToMove.Sign);
        }

        private bool transferToKing(Point i_Location, GameTool i_GameToolToMove)
        {
            bool result = false;

            if ((i_Location.X == Height - 1 || i_Location.X == 0) && !i_GameToolToMove.IsKing()) 
            {
                result = true;
            }

            return result;
        }

        private void addToBoard(Point i_PointToAdd, char i_Sign)
        {
            m_GameBoard[i_PointToAdd.X, i_PointToAdd.Y] = i_Sign;
        }

        public void DeleteFromBoard(Point i_PointToErase)
        {
            m_GameBoard[i_PointToErase.X, i_PointToErase.Y] = (char)GameTool.eSigns.Empty;
        }

        public void Clear()
        {
            for(int i = 0; i < Height; i++)
            {
                for(int j = 0; j < Width; j++)
                {
                    this[i, j] = (char)GameTool.eSigns.Empty;
                }
            }
        }

        public static bool validSize(string i_BoardSize, out int o_ValidBoardSize)
        {
            bool isNumeric = false;

            isNumeric = int.TryParse(i_BoardSize, out o_ValidBoardSize);
            return isNumeric && legalSize(o_ValidBoardSize);
        }

        public static bool legalSize(int i_ValidSize)
        {
            return i_ValidSize == (int)Board.eBoradSize.Small || i_ValidSize == (int)Board.eBoradSize.Medium || i_ValidSize == (int)Board.eBoradSize.Large;
        }
    }
}
