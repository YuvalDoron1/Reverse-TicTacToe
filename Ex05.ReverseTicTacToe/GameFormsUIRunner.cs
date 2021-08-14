namespace Ex05.ReverseTicTacToe
{
    public class GameFormsUIRunner
    {
        public static void Run()
        {
            SettingsForm settingsForm = new SettingsForm();
            settingsForm.ShowDialog();
            string player1Name = settingsForm.Player1Name;
            string player2Name = settingsForm.Player2Name;
            int boardDimensions = settingsForm.BoardDimensions;
            eGameMode gameMode = settingsForm.GameMode;
            GameLogicManager logicManager = new GameLogicManager(boardDimensions, gameMode, player1Name, player2Name);
            GameForm gameForm = new GameForm(logicManager);
            if (settingsForm.FormClosed)
            {
                gameForm.ShowDialog();
            }
        }
    }
}