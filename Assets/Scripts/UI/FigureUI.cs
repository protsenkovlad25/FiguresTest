using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FigureUI : MonoBehaviour
{
    [SerializeField] private Image _shapeImage;
    [SerializeField] private Image _foodImage;

    private FigureData _data;

    public FigureData Data => _data;

    public void SetData(FigureData data)
    {
        _data = data;

        _shapeImage.sprite = Configs.FiguresConfig.GetShapeSprite(data.ShapeType);
        _foodImage.sprite = Configs.FiguresConfig.GetFoodSprite(data.FoodType);
        _shapeImage.color = Configs.FiguresConfig.GetColor(data.ColorType);
    }

    public void DisableAnim()
    {
        Sequence s = DOTween.Sequence();

        s.Append(transform.DOScale(0, .5f));
        s.AppendCallback(() => { gameObject.SetActive(false); transform.localScale = Vector3.one; });
    }
}
