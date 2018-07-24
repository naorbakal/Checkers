using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Drawing;

namespace AmericanCheckers
{
    public class GameTool
    {
        public enum eDirection
        {
            Down = 1,
            Up = -1,
            Left = -1,
            Right = 1,
        }

        public enum eSigns
        {
            PlayerO = 'O',
            PlayerX = 'X',
            PlayerOKing = 'U',
            PlayerXKing = 'K',
            Empty = ' ',
        }

        public enum eValue
        {
            King = 4,
            Regular = 1,
        }

        private char m_Sign;
        private Point m_Location;
        private char m_FriendSign;

        public GameTool(Point point, char m_ToolShape)
        {
            m_Location = point;
            m_Sign = m_ToolShape;
            m_FriendSign = m_Sign == (char)eSigns.PlayerO ? (char)eSigns.PlayerOKing : (char)eSigns.PlayerXKing;
        }

        public char Sign
        {
            get
            {
                return m_Sign;
            }

            set
            {
                m_Sign = value;
            }
        }

        public Point Location
        {
            get
            {
                return m_Location;
            }

            set
            {
                m_Location = value;
            }
        }

        public char FriendSign
        {
            get
            {
                return m_FriendSign;
            }

            set
            {
                m_FriendSign = value;
            }
        }

        public int Value
        {
            get
            {
                return IsKing() ? (int)eValue.King : (int)eValue.Regular;              
            }
        }

        public bool CanMove(Board i_GameBoard, Point i_NextMove)
        {
            int nextMoveRow = i_NextMove.X, nextMoveCol = i_NextMove.Y;

            return !checkIfOutOfBounds(i_GameBoard, i_NextMove) && i_GameBoard[nextMoveRow, nextMoveCol] == (char)eSigns.Empty;
        }

        private bool checkIfOutOfBounds(Board i_GameBoard, Point i_NextMove)
        {
            int  nextMoveRow = i_NextMove.X, nextMoveCol = i_NextMove.Y;
            bool checkRow = (nextMoveRow >= i_GameBoard.Height) || (nextMoveRow < 0);
            bool checkCol = (nextMoveCol >= i_GameBoard.Width) || (nextMoveCol < 0);

            return checkRow || checkCol;
        }

        public bool IsKing()
        {
            return m_Sign == (char)eSigns.PlayerOKing || m_Sign == (char)eSigns.PlayerXKing;
        }

        public void AddRegularMoves(Board i_GameBoard, List<Move> i_List)
        {
            int directionMove = getDirectionMove();

            insertRegularMovesByDirection(i_GameBoard, directionMove, i_List);
            if (IsKing())
            {
                directionMove *= -1;
                insertRegularMovesByDirection(i_GameBoard, directionMove, i_List);
            }
        }

        private void insertRegularMovesByDirection(Board i_GameBoard, int directionMove, List<Move> i_List)
        {
            Point nextPointCheck = new Point(m_Location.X + directionMove, m_Location.Y + (int)eDirection.Left);

            if (CanMove(i_GameBoard, nextPointCheck))
            {
                addMoveToRegularList(nextPointCheck, i_List);
            }

            nextPointCheck = new Point(m_Location.X + directionMove, m_Location.Y + (int)eDirection.Right);
            if (CanMove(i_GameBoard, nextPointCheck))
            {
                addMoveToRegularList(nextPointCheck, i_List);
            }
        }

        private void addMoveToRegularList(Point i_nextPointCheck, List<Move> o_List)
        {
            Move moveToAdd = new Move();

            moveToAdd.From = m_Location;
            moveToAdd.To = i_nextPointCheck;
            o_List.Add(moveToAdd);
        }

        public void AddSkipMoves(Board i_GameBoard, List<Move> o_Result)
        {
            int directionMove;

            directionMove = getDirectionMove();
            insertSkipMovesByDirection(i_GameBoard, directionMove, o_Result);
            if (IsKing())
            {
                directionMove *= -1;
                insertSkipMovesByDirection(i_GameBoard, directionMove, o_Result);
            }
        }

        private void insertSkipMovesByDirection(Board i_GameBoard, int i_directionMove, List<Move> o_List)
        {
            Point nextPointCheck;
            Point eatenToolLocation;

            nextPointCheck = new Point(m_Location.X + i_directionMove, m_Location.Y + (int)eDirection.Left);
            if (isOpponent(i_GameBoard, nextPointCheck))
            {
                eatenToolLocation = new Point(m_Location.X + i_directionMove, m_Location.Y + (int)eDirection.Left);
                nextPointCheck = new Point(m_Location.X + (i_directionMove * 2), m_Location.Y + ((int)eDirection.Left * 2));
                if (CanMove(i_GameBoard, nextPointCheck))
                {
                    addMoveToSkipList(nextPointCheck, o_List, eatenToolLocation);
                }
            }

            nextPointCheck = new Point(m_Location.X + i_directionMove, m_Location.Y + (int)eDirection.Right);

            if (isOpponent(i_GameBoard, nextPointCheck))
            {
                eatenToolLocation = new Point(m_Location.X + i_directionMove, m_Location.Y + (int)eDirection.Right);
                nextPointCheck = new Point(m_Location.X + (i_directionMove * 2), m_Location.Y + ((int)eDirection.Right * 2));

                if (CanMove(i_GameBoard, nextPointCheck))
                {
                    addMoveToSkipList(nextPointCheck, o_List, eatenToolLocation);
                }
            }
        }

        private void addMoveToSkipList(Point i_NextLocation, List<Move> o_List, Point i_EatenToolLocation)
        {
            Move moveToAdd = new Move();

            moveToAdd.From = m_Location;
            moveToAdd.To = i_NextLocation;
            moveToAdd.Eaten = i_EatenToolLocation;
            moveToAdd.IsSkipMove = true;
            o_List.Add(moveToAdd);
        }

        private bool isOpponent(Board i_GameBoard, Point i_Location)
        {
            bool result = false;
         
            if (!checkIfOutOfBounds(i_GameBoard, i_Location))
            {
                char sign = i_GameBoard[i_Location.X, i_Location.Y];
                if (sign != m_Sign && sign != m_FriendSign && sign != (char)eSigns.Empty)
                {
                    result = true;
                }
            }

            return result;
        }

        private int getDirectionMove()
        {
            return (m_Sign == (char)eSigns.PlayerO || m_Sign == (char)eSigns.PlayerOKing) ? (int)eDirection.Down : (int)eDirection.Up;
        }
    }
}
