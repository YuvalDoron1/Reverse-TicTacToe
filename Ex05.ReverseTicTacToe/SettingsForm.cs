using System.Windows.Forms;
using System.Drawing;

namespace Ex05.ReverseTicTacToe
{
    public class SettingsForm : Form
    {
        private Label m_PlayersLabel = new Label();
        private Label m_Player1Label = new Label();
        private Label m_Player2Label = new Label();
        private Label m_BoardSizeLabel = new Label();
        private Label m_RowsLabel = new Label();
        private Label m_ColsLabel = new Label();
        private TextBox m_Player1NameTextBox = new TextBox();
        private TextBox m_Player2NameTextBox = new TextBox();
        private CheckBox m_Player2CheckBox = new CheckBox();
        private NumericUpDown m_RowsUpDown = new NumericUpDown();
        private NumericUpDown m_ColsUpDown = new NumericUpDown();
        private Button m_Submission = new Button();
        private bool m_FormClosed = false;

        public SettingsForm()
        {
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Size = new Size(300, 300);
            this.Text = "Game Settings";
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.m_PlayersLabel.Text = "Players:";
            this.m_PlayersLabel.Location = new Point(10, 10);
            this.m_Player1Label.Text = "Player 1:";
            this.m_Player1Label.Location = new Point(m_PlayersLabel.Left + 20, m_PlayersLabel.Bottom + 5);
            this.m_Player1NameTextBox.Location = new Point(m_Player1Label.Right, m_Player1Label.Top);
            this.m_Player1NameTextBox.Width = 100;
            this.m_Player2CheckBox.Location = new Point(m_Player1Label.Left, m_Player1Label.Bottom);
            this.m_Player2CheckBox.Width = 10;
            this.m_Player2CheckBox.CheckedChanged += m_Player2CheckBox_CheckedChanged;
            this.m_Player2Label.Text = "Player 2:";
            this.m_Player2Label.Width = 80;
            this.m_Player2Label.Location = new Point(50, m_Player2CheckBox.Top + 5);
            this.m_Player2NameTextBox.Text = "Computer";
            this.m_Player2NameTextBox.Location = new Point(m_Player2Label.Right, m_Player2Label.Top);
            this.m_Player2NameTextBox.Enabled = false;
            this.m_Player2NameTextBox.Width = 100;
            this.m_BoardSizeLabel.Text = "Board Size:";
            this.m_BoardSizeLabel.Location = new Point(m_PlayersLabel.Left, m_Player2Label.Bottom + 30);
            this.m_RowsLabel.Text = "Rows:";
            this.m_RowsLabel.Width = 40;
            this.m_RowsLabel.TextAlign = ContentAlignment.MiddleCenter;

            this.m_RowsLabel.Location = new Point(m_BoardSizeLabel.Left + 20, m_BoardSizeLabel.Bottom + 5);
            this.m_RowsUpDown.Location = new Point(m_RowsLabel.Right, m_RowsLabel.Top);
            this.m_RowsUpDown.Width = 40;
            this.m_RowsUpDown.Minimum = 3;
            this.m_RowsUpDown.Maximum = 9;
            this.m_RowsUpDown.ValueChanged += m_RowsUpDown_ValueChanged;

            this.m_ColsLabel.Text = "Cols:";
            this.m_ColsLabel.TextAlign = ContentAlignment.MiddleCenter;

            this.m_ColsLabel.Location = new Point(m_RowsUpDown.Right + 10, m_RowsLabel.Top);
            this.m_ColsLabel.Width = 40;
            this.m_ColsUpDown.Location = new Point(m_ColsLabel.Right, m_RowsLabel.Top);
            this.m_ColsUpDown.Width = 40;
            this.m_ColsUpDown.Minimum = 3;
            this.m_ColsUpDown.Maximum = 9;
            this.m_ColsUpDown.ValueChanged += m_ColUpDown_ValueChanged;
            this.m_Submission.Width = 250;
            this.m_Submission.Text = "Start!";
            this.m_Submission.Location = new Point(this.Left + 20, 200);
            this.m_Submission.Click += m_Submission_Click;
            this.Controls.AddRange(new Control[] { m_PlayersLabel, m_Player1Label, m_Player1NameTextBox, m_Player2CheckBox, m_Player2Label, m_Player2NameTextBox, m_BoardSizeLabel, m_RowsLabel, m_RowsUpDown, m_ColsLabel, m_ColsUpDown, m_Submission });
        }

        private void m_Submission_Click(object sender, System.EventArgs e)
        {
            m_FormClosed = true;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void m_RowsUpDown_ValueChanged(object sender, System.EventArgs e)
        {
            this.m_ColsUpDown.Value = (sender as NumericUpDown).Value;
        }

        private void m_ColUpDown_ValueChanged(object sender, System.EventArgs e)
        {
            this.m_RowsUpDown.Value = (sender as NumericUpDown).Value;
        }

        private void m_Player2CheckBox_CheckedChanged(object sender, System.EventArgs e)
        {
            m_Player2NameTextBox.Enabled = !m_Player2NameTextBox.Enabled;
            m_Player2NameTextBox.Text = m_Player2NameTextBox.Enabled ? "" : "Computer";
        }

        public int BoardDimensions
        {
            get
            {
                return (int)m_RowsUpDown.Value;
            }
        }
        public eGameMode GameMode
        {
            get
            {
                return m_Player2CheckBox.Checked ? eGameMode.VsPlayer : eGameMode.VsComputer;
            }
        }

        public string Player1Name
        {
            get
            {
                return m_Player1NameTextBox.Text;
            }
        }

        public string Player2Name
        {
            get
            {
                return m_Player2NameTextBox.Text;
            }
        }

        public bool FormClosed
        {
            get
            {
                return m_FormClosed;
            }
        }
    }
}