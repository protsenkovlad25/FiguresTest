using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FigureController
{
    public static event UnityAction OnFiguresOver;
    public static event UnityAction OnBarOver;

    private ActionBarController _barController;

    private FigureSpawner _spawner;
    private FigureGenerator _generator;

    private Transform _gameParent;

    private List<Figure> _activeFigures;
    private List<Figure> _barFigures;

    public FigureController(FigureSpawner spawner, FigureGenerator generator, ActionBarController bar, Transform gameParent)
    {
        _spawner = spawner;
        _generator = generator;
        _barController = bar;

        _gameParent = gameParent;

        _activeFigures = new List<Figure>();
        _barFigures = new List<Figure>();

        Figure.OnClicked += ClickFigure;
        ActionBarController.OnAddAnimEnd += CheckBarFigures;
    }

    public void ClickFigure(Figure figure)
    {
        if (!_barController.IsBarFull)
        {
            _barController.AddFigureToBar(figure);
            _barFigures.Add(figure);

            figure.DisableAnim();

            _activeFigures.Remove(figure);
            if (_activeFigures.Count == 0)
                OnFiguresOver?.Invoke();
        }
    }

    public void CheckBarFigures()
    {
        Dictionary<FigureData, int> similars = new Dictionary<FigureData, int>();

        foreach (var figure in _barFigures)
        {
            if (similars.TryGetValue(figure.Data, out _))
                similars[figure.Data]++;
            else
                similars.Add(figure.Data, 1);
        }

        List<FigureData> removers = new List<FigureData>();
        foreach (var sim in similars)
        {
            if (sim.Value == 3)
            {
                _barController.RemoveFigureFromBar(sim.Key);
                removers.Add(sim.Key);
            }
        }

        if (removers.Count > 0)
            for (int i = _barFigures.Count - 1; i >= 0; i--)
                if (removers.Contains(_barFigures[i].Data))
                    _barFigures.RemoveAt(i);

        if (_barController.IsBarFull)
            OnBarOver?.Invoke();
    }

    public void GenerateFigures(int count)
    {
        _activeFigures = _generator.GenerateFigures(count);

        foreach (var figure in _activeFigures)
        {
            figure.transform.parent = _gameParent;
            figure.Init();
        }

        _spawner.SpawnFigures(_activeFigures);
    }
    public void RegenerateFigures()
    {
        int count = _activeFigures.Count + _barFigures.Count;

        Clear();
        GenerateFigures(count);
    }

    public void Clear()
    {
        foreach (var figure in _activeFigures)
        {
            figure.DisableAnim();
        }

        _barController.Clear();
        _activeFigures.Clear();
        _barFigures.Clear();
    }
}
