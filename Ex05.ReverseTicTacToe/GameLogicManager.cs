using System;
using System.Collections.Generic;

namespace Ex05.ReverseTicTacToe
{
    public class GameLogicManager
    {
        public Action<int, int> CellChangedByComp;
        private readonly GameBoard r_Board;
        private readonly eGameMode r_GameMode;
        private readonly Player r_Player1;
        private readonly Player r_Player2;
        private Player m_CurrentPlayer;
        private Random r_RandomMachine;

        public GameLogicManager(int i_BoardDimensions, eGameMode i_GameMode, string i_Player1Name, string i_Player2Name)
        {
            r_Board = new GameBoard(i_BoardDimensions);
            r_GameMode = i_GameMode;
            r_Player1 = new Player(eGameSign.X, i_Player1Name);
            r_Player2 = new Player(eGameSign.O, i_Player2Name);
            m_CurrentPlayer = r_Player1;
            r_RandomMachine = new Random();
        }

        public void ResetNewGame()
        {
            m_CurrentPlayer = r_Player1;
            r_Board.InitializeBoard();
        }

        public GameBoard Board
        {
            get
            {
                return r_Board;
            }
        }

        public Player CurrentPlayer
        {
            get
            {
                return m_CurrentPlayer;
            }
        }

        public Player Player1
        {
            get
            {
                return r_Player1;
            }
        }

        public Player Player2
        {
            get
            {
                return r_Player2;
            }
        }

        public eGameMode GameMode
        {
            get
            {
                return r_GameMode;
            }
        }

        public void SwitchTurn()
        {
            m_CurrentPlayer = (m_CurrentPlayer == r_Player1) ? r_Player2 : r_Player1;
        }

        public static bool ValidBoardDimensions(int i_SelectedDimensions)
        {
            return (i_SelectedDimensions >= 3 && i_SelectedDimensions <= 9);
        }

        public void GenerateRandomMove()
        {
            List<int> places = r_Board.EmptyPlaces;
            int listElementsNumber = places.Count;
            int place = places[r_RandomMachine.Next(listElementsNumber)];
            int row = place / r_Board.Size;
            int col = place % r_Board.Size;

            if (CellChangedByComp != null)
            {
                CellChangedByComp.Invoke(row, col);
            }
        }

        public eGameStatus CheckGameStatus(out bool o_IsRoundEnds)
        {
            eGameStatus gameStatus;
            o_IsRoundEnds = true;

            if (r_Board.IsPlayerLost(r_Player1.Sign))
            {
                r_Player2.Wins++;
                gameStatus = eGameStatus.Player2Wins;
            }
            else if (r_Board.IsPlayerLost(r_Player2.Sign))
            {
                r_Player1.Wins++;
                gameStatus = eGameStatus.Player1Wins;
            }
            else if (r_Board.IsBoardFull())
            {
                gameStatus = eGameStatus.Tie;
            }
            else
            {
                o_IsRoundEnds = false;
                gameStatus = eGameStatus.Alive;
            }

            return gameStatus;
        }
    }
}