using System.Collections.Generic;

namespace Ex05.ReverseTicTacToe
{
    public class GameBoard
    {
        private readonly eGameSign[,] r_Board;
        private readonly int r_Size;
        private List<int> m_EmptyPlaces;

        public GameBoard(int i_BoardLength)
        {
            r_Board = new eGameSign[i_BoardLength, i_BoardLength];
            r_Size = i_BoardLength;
        }

        public void InitializeBoard()
        {
            for (int i = 0; i < r_Size; i++)
            {
                for (int j = 0; j < r_Size; j++)
                {
                    r_Board[i, j] = eGameSign.Blank;
                }
            }

            initializeEmptyPlaces();
        }

        private void initializeEmptyPlaces()
        {
            m_EmptyPlaces = new List<int>();
            for (int i = 0; i < r_Size * r_Size; i++)
            {
                m_EmptyPlaces.Add(i);
            }
        }

        public List<int> EmptyPlaces
        {
            get
            {
                return m_EmptyPlaces;
            }
        }

        public int Size
        {
            get
            {
                return r_Size;
            }
        }

        public eGameSign[,] Board
        {
            get
            {
                return r_Board;
            }
        }

        public bool IsBoardFull()
        {
            return (m_EmptyPlaces.Count == 0);
        }

        public bool IsValidLineOrRow(int i_PlayerChoice)
        {
            return (i_PlayerChoice > 0 && i_PlayerChoice <= r_Size);
        }

        public bool IsEmptyCell(int i_Row, int i_Col)
        {
            return (r_Board[i_Row, i_Col] == eGameSign.Blank);
        }

        public void InsertChar(eGameSign i_Sign, int i_Row, int i_Col)
        {
            r_Board[i_Row, i_Col] = i_Sign;
            m_EmptyPlaces.Remove((i_Row * r_Size) + i_Col);
        }

        public bool IsPlayerLost(eGameSign i_Sign)
        {
            return (isSomeColFilledWithSign(i_Sign) || isSomeRowFilledWithSign(i_Sign) || isSomeDiagonalFilledWithSign(i_Sign));
        }

        private bool isSomeRowFilledWithSign(eGameSign i_Sign)
        {
            bool isRowFilledWithSign = false;

            for (int i = 0; i < r_Size; i++)
            {
                for (int j = 0; j < r_Size; j++)
                {
                    if (r_Board[i, j] == i_Sign && j == r_Size - 1)
                    {
                        isRowFilledWithSign = true;
                    }
                    else if (r_Board[i, j] != i_Sign)
                    {
                        break;
                    }
                }

                if (isRowFilledWithSign)
                {
                    break;
                }
            }

            return isRowFilledWithSign;
        }

        private bool isSomeColFilledWithSign(eGameSign i_Sign)
        {
            bool isColFilledWithSign = false;

            for (int i = 0; i < r_Size; i++)
            {
                for (int j = 0; j < r_Size; j++)
                {
                    if (r_Board[j, i] == i_Sign && j == r_Size - 1)
                    {
                        isColFilledWithSign = true;
                    }
                    else if (r_Board[j, i] != i_Sign)
                    {
                        break;
                    }
                }

                if (isColFilledWithSign)
                {
                    break;
                }
            }

            return isColFilledWithSign;
        }

        private bool isSomeDiagonalFilledWithSign(eGameSign i_Sign)
        {
            bool isDigonalFilledWithSign = false;

            for (int i = 0; i < r_Size; i++)
            {
                if (r_Board[i, i] == i_Sign && i == r_Size - 1)
                {
                    isDigonalFilledWithSign = true;
                }
                else if (r_Board[i, i] != i_Sign)
                {
                    break;
                }
            }

            if (!isDigonalFilledWithSign)
            {
                for (int i = 0; i < r_Size; i++)
                {
                    if (r_Board[r_Size - i - 1, i] == i_Sign && i == r_Size - 1)
                    {
                        isDigonalFilledWithSign = true;
                    }
                    else if (r_Board[r_Size - i - 1, i] != i_Sign)
                    {
                        break;
                    }
                }
            }

            return isDigonalFilledWithSign;
        }
    }
}