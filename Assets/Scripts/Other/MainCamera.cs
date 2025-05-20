using UnityEngine;
using Zenject;

public class MainCamera : MonoBehaviour, IInitializable
{
    [SerializeField] private Canvas _canvas;

    private Camera _camera;

    public Camera Camera => _camera;
    public Canvas Canvas => _canvas;

    public void Initialize()
    {
        _camera = Camera.main;
    }
}
