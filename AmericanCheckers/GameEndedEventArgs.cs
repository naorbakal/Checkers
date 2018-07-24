using System;
using System.Collections.Generic;
using System.Text;

namespace AmericanCheckers
{
    public class GameEndedEventArgs : EventArgs
    {
        private string m_Message;

        public GameEndedEventArgs(string i_Message)
        {
            m_Message = i_Message;
        }

        public string Message
        {
            get
            {
                return m_Message;
            }
        }           
    }
}
