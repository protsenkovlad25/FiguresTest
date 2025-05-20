public class GameController
{
    private FigureController _figureController;
    private GameOverPanel _gameOverPanel;

    public GameController(FigureController figureController, GameOverPanel gameOverPanel)
    {
        _figureController = figureController;
        
        _gameOverPanel = gameOverPanel;
        _gameOverPanel.OnRestartClick += RestartGame;
        _gameOverPanel.Init();

        FigureController.OnBarOver += LoseGame;
        FigureController.OnFiguresOver += WinGame;
        ResetButton.OnClicked += Regenerate;

        StartGame();
    }

    public void StartGame()
    {
        _figureController.GenerateFigures(Configs.FiguresConfig.Count);
    }
    public void RestartGame()
    {
        _gameOverPanel.Close();

        _figureController.Clear();

        StartGame();
    }

    public void LoseGame()
    {
        _gameOverPanel.Open(false);
    }
    public void WinGame()
    {
        _gameOverPanel.Open(true);
    }

    public void Regenerate()
    {
        _figureController.RegenerateFigures();
    }
}
