using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class ActionBarController
{
    public static event UnityAction OnAddAnimEnd;

    private ActionBar _bar;
    private MainCamera _mainCamera;
    private FigureUI _figureUIPrefab;

    public bool IsBarFull => !_bar.ExistFreeSlot;

    public ActionBarController(MainCamera camera, ActionBar bar)
    {
        _bar = bar;
        _bar.Init();

        _mainCamera = camera;

        _figureUIPrefab = Configs.FiguresConfig.FigureUIPrefab;
    }

    public void AddFigureToBar(Figure figure)
    {
        FigureUI figureUI = CreateFigureUI(figure);
        ActionSlot freeSlot = _bar.GetFreeSlot();

        freeSlot.SetData(figure.Data);

        AddAnim(figureUI, freeSlot);
    }
    public void RemoveFigureFromBar(FigureData data)
    {
        _bar.RemoveFigures(data);
    }

    private void AddAnim(FigureUI figure, ActionSlot slot)
    {
        Sequence s = DOTween.Sequence();

        s.Append(figure.transform.DOMove(slot.transform.position, .5f));
        s.AppendCallback(() =>
        {
            slot.ChangeActivity(true);
            DestroyFigureUI(figure);
            OnAddAnimEnd?.Invoke();
        });
    }

    private FigureUI CreateFigureUI(Figure figure)
    {
        FigureUI figureUI = Object.Instantiate(_figureUIPrefab, _mainCamera.Canvas.transform);

        figureUI.transform.localPosition = GetScreenFigurePosition(figure.transform.position);
        figureUI.SetData(figure.Data);

        return figureUI;
    }
    private void DestroyFigureUI(FigureUI figure)
    {
        Object.Destroy(figure.gameObject);
    }

    private Vector2 GetScreenFigurePosition(Vector3 position)
    {
        Vector3 screenPos = _mainCamera.Camera.WorldToScreenPoint(position);
        RectTransform canvasRect = _mainCamera.Canvas.GetComponent<RectTransform>();

        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPos, _mainCamera.Camera, out Vector2 localPoint);

        return localPoint;
    }

    public void Clear()
    {
        _bar.ClearSlots();
    }
}
