using System;
using System.Collections.Generic;
using System.Text;

namespace UIWindows
{
    public class MessageBoxAnsweredEventArgs : EventArgs
    {
        private bool m_IsAnsweredYes;

        public MessageBoxAnsweredEventArgs(bool i_IsAnsweredYes)
        {
            m_IsAnsweredYes = i_IsAnsweredYes;
        }

        public bool IsAnsweredYes
        {
            get
            {
                return m_IsAnsweredYes;
            }
        }
    }
}
