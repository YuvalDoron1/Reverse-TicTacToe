using System.Windows.Forms;
using System.Drawing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ex05.ReverseTicTacToe
{
    public class GameForm : Form
    {
        // nested class of a game button - a button that has row and column associated with him
        public class GameButton : Button
        {
            private readonly int r_Row;
            private readonly int r_Col;

            public GameButton(int i_Row, int i_Col)
            {
                r_Row = i_Row;
                r_Col = i_Col;
                this.SetStyle(ControlStyles.Selectable, false);
            }

            public int Row
            {
                get
                {
                    return r_Row;
                }
            }

            public int Col
            {
                get
                {
                    return r_Col;
                }
            }
        }

        private readonly GameLogicManager r_gameManager;
        private readonly List<GameButton> r_BoardButtons;
        private Label m_Player1ScoreLabel = new Label();
        private Label m_Player2ScoreLabel = new Label();
        private const int k_ButtonLength = 50;
        private const int k_ButtonWidth = 50;
        private const int k_Indentation = 10;
        private const int k_BottomSpace = 80;

        public GameForm(GameLogicManager i_GameManager)
        {
            r_gameManager = i_GameManager;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Size = getFormSize();
            this.Text = "TicTacToeMisere";
            r_BoardButtons = new List<GameButton>();
            r_gameManager.CellChangedByComp += GameCellOfComp_Changed;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            initializeButtons();
            initializeLabels();
        }

        private Size getFormSize()
        {
            Size formSize;
            int boardSize = r_gameManager.Board.Size;
            int length = boardSize * (k_ButtonLength + k_Indentation) + k_BottomSpace;
            int width = boardSize * (k_ButtonWidth + k_Indentation) + 2 * k_Indentation; //need to fix the right side!
            formSize = new Size(width, length);

            return formSize;
        }

        private void initializeLabels()
        {
            this.m_Player1ScoreLabel.Text = $"{r_gameManager.Player1.Name}: {r_gameManager.Player1.Wins}"; //change to real name and make it bold
            this.m_Player1ScoreLabel.Font = new Font(Label.DefaultFont, FontStyle.Bold);
            this.m_Player1ScoreLabel.Top = this.Size.Height - k_BottomSpace + k_Indentation;
            this.m_Player1ScoreLabel.AutoSize = false;
            this.m_Player1ScoreLabel.Padding = new Padding(0);
            this.m_Player1ScoreLabel.Margin = new Padding(0);
            this.m_Player1ScoreLabel.TextAlign = ContentAlignment.TopRight;
            this.m_Player1ScoreLabel.Left = this.Size.Width / 2 - m_Player1ScoreLabel.Width;
            this.m_Player2ScoreLabel.Text = $"{r_gameManager.Player2.Name}: {r_gameManager.Player2.Wins}";
            this.m_Player2ScoreLabel.AutoSize = false;
            this.m_Player2ScoreLabel.Padding = new Padding(0);
            this.m_Player2ScoreLabel.Margin = new Padding(0);
            this.m_Player2ScoreLabel.Top = m_Player1ScoreLabel.Top;
            this.m_Player2ScoreLabel.Left = this.Size.Width / 2 + k_Indentation;
            this.Controls.AddRange(new Control[] { m_Player1ScoreLabel, m_Player2ScoreLabel });
        }

        private void initializeButtons()
        {
            Size buttonSize = new Size(k_ButtonWidth, k_ButtonLength);
            for (int i = 0; i < r_gameManager.Board.Size; i++)
            {
                for (int j = 0; j < r_gameManager.Board.Size; j++)
                {
                    GameButton gameButton = new GameButton(i, j);
                    gameButton.Size = buttonSize;
                    gameButton.Top = k_Indentation + (i * (k_ButtonLength + k_Indentation));
                    gameButton.Left = k_Indentation + (j * (k_ButtonWidth + k_Indentation));
                    gameButton.Click += gameButton_Click;
                    this.Controls.Add(gameButton);
                    r_BoardButtons.Add(gameButton);
                }
            }

            r_gameManager.ResetNewGame();
        }

        private void gameButton_Click(object sender, EventArgs e)
        {
            GameButton clickedButton = (sender as GameButton);
            clickedButton.Text = r_gameManager.CurrentPlayer.Sign.Equals(eGameSign.X) ? "x" : "o";
            clickedButton.Enabled = false;
            r_gameManager.Board.InsertChar(r_gameManager.CurrentPlayer.Sign, clickedButton.Row, clickedButton.Col);
            checkGameStatus();
        }

        private void checkGameStatus()
        {
            eGameStatus currentGameStatus = r_gameManager.CheckGameStatus(out bool finished);
            if (finished)
            {
                StringBuilder roundEndMessage = new StringBuilder();
                string roundEndTitle = "A Win!";
                switch (currentGameStatus)
                {
                    case eGameStatus.Player1Wins:
                        roundEndMessage.Append($"The winner is {r_gameManager.Player1.Name}");
                        break;
                    case eGameStatus.Player2Wins:
                        roundEndMessage.Append($"The winner is {r_gameManager.Player2.Name}");
                        break;
                    case eGameStatus.Tie:
                        roundEndMessage.Append($"Tie!");
                        roundEndTitle = "A Tie!";
                        break;
                }

                roundEndMessage.Append($"{Environment.NewLine}Would you like to play another round?");
                DialogResult diaglogResult = MessageBox.Show(roundEndMessage.ToString(), roundEndTitle, MessageBoxButtons.YesNo);
                switch (diaglogResult)
                {
                    case DialogResult.Yes:
                        resetGameWindow();
                        r_gameManager.ResetNewGame();
                        break;
                    case DialogResult.No:
                        this.Close();
                        break;
                }
            }
            else
            {
                switchTurn();
            }
        }

        private void switchTurn()
        {
            r_gameManager.SwitchTurn();
            if (m_Player1ScoreLabel.Font.Bold)
            {
                m_Player1ScoreLabel.Font = new Font(Label.DefaultFont, FontStyle.Regular);
                m_Player2ScoreLabel.Font = new Font(Label.DefaultFont, FontStyle.Bold);
            }
            else
            {
                m_Player1ScoreLabel.Font = new Font(Label.DefaultFont, FontStyle.Bold);
                m_Player2ScoreLabel.Font = new Font(Label.DefaultFont, FontStyle.Regular);
            }

            eGameMode gameMode = r_gameManager.GameMode;
            bool VsComputer = gameMode.Equals(eGameMode.VsComputer);
            if (VsComputer)
            {
                if (r_gameManager.CurrentPlayer.Sign.Equals(eGameSign.O))
                {
                    r_gameManager.GenerateRandomMove();
                }
            }
        }

        public void GameCellOfComp_Changed(int i_Row, int i_Col)
        {
            int boardSize = r_gameManager.Board.Size;
            int index = (i_Row * boardSize) + i_Col;
            GameButton computerButton = r_BoardButtons[index];
            gameButton_Click(computerButton, null);
        }

        private void resetGameWindow()
        {
            foreach (GameButton gameButton in r_BoardButtons)
            {
                gameButton.Enabled = true;
                gameButton.Text = "";
            }

            if (m_Player2ScoreLabel.Font.Bold)
            {
                m_Player1ScoreLabel.Font = new Font(Label.DefaultFont, FontStyle.Bold);
                m_Player2ScoreLabel.Font = new Font(Label.DefaultFont, FontStyle.Regular);
            }

            this.m_Player1ScoreLabel.Text = $"{r_gameManager.Player1.Name}: {r_gameManager.Player1.Wins}";
            this.m_Player2ScoreLabel.Text = $"{r_gameManager.Player2.Name}: {r_gameManager.Player2.Wins}";
        }
    }
}