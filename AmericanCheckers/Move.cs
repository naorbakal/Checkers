using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace AmericanCheckers
{    
    public class Move
    {
        private Point m_From;
        private Point m_To;
        private Point m_Eaten;
        private bool m_isSkipMove = false;

        public Move()
        {
        }

        public Move(Point i_From, Point i_To)
        {
            m_From = i_From;
            m_To = i_To;
        }

        public Point From
        {
            get
            {
                return m_From;
            }

            set
            {
                m_From = value;
            }
        }

        public Point To
        {
            get
            {
                return m_To;
            }

            set
            {
                m_To = value;
            }
        }

        public Point Eaten
        {
            get
            {
                return m_Eaten;
            }

            set
            {
                m_Eaten = value;
            }
        }

        public bool IsSkipMove
        {
            get
            {
                return m_isSkipMove;
            }

            set
            {
                m_isSkipMove = value;
            }
        }

        public static bool FindMoveInList(ref Move io_inputMove, List<Move> io_List)
        {
            bool result = false;

            foreach(Move currentMove in io_List)
            {
                if (isEqualLocation(currentMove, io_inputMove))
                {
                    result = true;
                    io_inputMove = currentMove;
                    break;
                }
            }

            return result;
        }

        private static bool isEqualLocation(Move i_Move, Move i_inputMove)
        {
            return i_Move.From == i_inputMove.From && i_Move.To == i_inputMove.To;
        }

        public GameTool Execute(Board i_GameBoard, List<GameTool> i_CurrentPlayerTools, List<GameTool> i_NextPlayerTools)
        {
            GameTool gameToolToMove, gameToolToErase;

            gameToolToMove = findGameToolByLocation(i_CurrentPlayerTools, m_From);
            gameToolToMove.Location = m_To;
            i_GameBoard.UpdateMoveOnBoard(this, gameToolToMove);

            if(IsSkipMove)
            {
                gameToolToErase = findGameToolByLocation(i_NextPlayerTools, m_Eaten);
                i_GameBoard.DeleteFromBoard(m_Eaten);
                i_NextPlayerTools.Remove(gameToolToErase);
            }

            return gameToolToMove;
        }

        private GameTool findGameToolByLocation(List<GameTool> i_CurrentPlayerTools, Point i_GameToolLocation)
        {
            GameTool result = null;

            foreach(GameTool currentGameTool in i_CurrentPlayerTools)
            {
                if(currentGameTool.Location == i_GameToolLocation)
                {
                    result = currentGameTool;
                    break;
                }
            }

            return result;
        }
    }
}
