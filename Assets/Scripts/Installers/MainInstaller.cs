using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    [Header("Transforms")]
    [SerializeField] private Transform _mainFiguresParent;
    [SerializeField] private Transform _gameFiguresParent;
    [SerializeField] private Transform _spawnPoint;

    [Header("Other")]
    [SerializeField] private ActionBar _actionBar;
    [SerializeField] private GameOverPanel _gameOverPanel;
    [SerializeField] private ResetButton _resetButton;

    public override void InstallBindings()
    {
        Application.targetFrameRate = 120;

        Container.BindInterfacesAndSelfTo<MainCamera>().FromComponentInHierarchy().AsSingle().NonLazy();

        Container.Bind<ActionBarController>().AsSingle().WithArguments(_actionBar).NonLazy();

        Container.BindInterfacesAndSelfTo<FigureSpawner>().AsSingle().WithArguments(_spawnPoint).NonLazy();
        Container.Bind<FigureGenerator>().AsSingle().WithArguments(_mainFiguresParent).NonLazy();
        Container.Bind<FigureController>().AsSingle().WithArguments(_gameFiguresParent).NonLazy();
        Container.Bind<GameController>().AsSingle().WithArguments(_gameOverPanel).NonLazy();
    }
}