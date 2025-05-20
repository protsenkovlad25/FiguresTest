using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ResetButton : MonoBehaviour
{
    public static event UnityAction OnClicked;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();

        FigureSpawner.OnStartSpawn += () => { ChangeInteractable(false); };
        FigureSpawner.OnEndSpawn += () => { ChangeInteractable(true); };
    }

    public void Click()
    {
        OnClicked?.Invoke();
    }

    public void ChangeInteractable(bool state) => _button.interactable = state;
}
