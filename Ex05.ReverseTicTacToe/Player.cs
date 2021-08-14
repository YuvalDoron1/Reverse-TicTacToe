namespace Ex05.ReverseTicTacToe
{
    public class Player
    {
        private readonly eGameSign r_PlayerSign;
        private readonly string r_Name;
        private int m_Wins;

        public Player(eGameSign i_Sign, string i_Name)
        {
            r_PlayerSign = i_Sign;
            r_Name = i_Name;
            m_Wins = 0;
        }

        public int Wins
        {
            get
            {
                return m_Wins;
            }

            set
            {
                m_Wins = value;
            }
        }

        public string Name
        {
            get
            {
                return r_Name;
            }
        }

        public eGameSign Sign
        {
            get
            {
                return r_PlayerSign;
            }
        }
    }
}