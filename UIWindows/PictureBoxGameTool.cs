using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace UIWindows
{
    public class PictureBoxGameTool : PictureBox
    {
        private Point m_PlaceOnBoard;
        private bool m_IsEnabled;

        public PictureBoxGameTool(int i_Row, int i_Col)
        {
            m_PlaceOnBoard = new Point(i_Row, i_Col);
        }

        public Point PlaceOnBoard
        {
            get
            {
                return m_PlaceOnBoard;
            }
        }

        public bool IsEnabled
        {
            get
            {
                return m_IsEnabled;
            }

            set
            {
                m_IsEnabled = value;
            }
        }
    }
}
