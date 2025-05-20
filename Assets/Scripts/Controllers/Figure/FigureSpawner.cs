using Proc;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class FigureSpawner : ITickable
{
    public static event UnityAction OnStartSpawn;
    public static event UnityAction OnEndSpawn;

    private Transform _spawnPoint;

    private Timer _spawnTimer;
    private List<Figure> _spawnFigures;

    public FigureSpawner(Transform spawnPoint)
    {
        _spawnPoint = spawnPoint;
    }

    public void SpawnFigures(List<Figure> figures)
    {
        _spawnFigures = new List<Figure>();
        _spawnFigures.AddRange(figures);

        foreach (var figure in figures)
        {
            figure.gameObject.SetActive(false);
            figure.transform.position = _spawnPoint.position + new Vector3(Random.Range(-0.3f, 0.3f), 0, 0);
            figure.transform.localScale = Vector3.one * GetEntityScale();
        }

        StartTimer();

        OnStartSpawn?.Invoke();
    }
    private void SpawnFigure()
    {
        if (_spawnFigures.Count > 0)
        {
            _spawnFigures[0].gameObject.SetActive(true);
            _spawnFigures[0].SpawnAnim();

            _spawnFigures.RemoveAt(0);

            ResetTimer();
        }
        else EndSpawn();
    }
    private void EndSpawn()
    {
        OnEndSpawn?.Invoke();
    }

    private float GetEntityScale()
    {
        int baseCount = 21;
        int minCount = 3;
        int maxCount = 63;

        float minScale = 0.6f;
        float maxScale = 2f;

        int count = Configs.FiguresConfig.Count;

        if (count == baseCount)
            return 1f;

        if (count < baseCount)
        {
            float t = Mathf.InverseLerp(minCount, baseCount, count);
            return Mathf.Lerp(maxScale, 1f, t);
        }
        else
        {
            float t = Mathf.InverseLerp(baseCount, maxCount, count);
            return Mathf.Lerp(1f, minScale, t);
        }
    }

    private void StartTimer()
    {
        _spawnTimer = new Timer(0.2f);
        _spawnTimer.OnTimesUp.AddListener(SpawnFigure);
    }
    private void ResetTimer()
    {
        _spawnTimer.Reset();
    }

    public void Tick()
    {
        _spawnTimer?.Update();
    }
}
