using UnityEngine;

public class ActionSlot : MonoBehaviour
{
    [SerializeField] private FigureUI _figure;

    private bool _isFree;

    public bool IsFree => _isFree;
    public FigureData Data => _figure.Data;

    public void Init()
    {
        _isFree = true;
    }

    public void ChangeActivity(bool state)
    {
        if (state)
            _figure.gameObject.SetActive(state);
        else
            _figure.DisableAnim();
    }

    public void SetData(FigureData data)
    {
        _isFree = false;

        _figure.SetData(data);
    }
    public void RemoveData()
    {
        _isFree = true;
    }
}
