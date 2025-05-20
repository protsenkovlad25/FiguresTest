using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class Figure : MonoBehaviour
{
    public static event UnityAction<Figure> OnClicked;
    public static event UnityAction<Figure> OnDisabled;

    [SerializeField] private SpriteRenderer _foodSR;
    [SerializeField] private SpriteRenderer _shapeSR;
    [SerializeField] private Collider2D _collider;

    private Rigidbody2D _rb;
    private FigureData data;

    public FigureData Data => data;

    public void Init()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void SetData(FigureData data)
    {
        this.data = data;

        _foodSR.sprite = Configs.FiguresConfig.GetFoodSprite(data.FoodType);
        _shapeSR.color = Configs.FiguresConfig.GetColor(data.ColorType);
    }

    public void SpawnAnim()
    {
        float scale = transform.localScale.x;
        transform.localScale = Vector3.zero;

        Sequence s = DOTween.Sequence();

        s.AppendCallback(() => { transform.DOScale(0, 0); });
        s.Append(transform.DOScale(scale, .2f));
    }
    public void DisableAnim()
    {
        Sequence s = DOTween.Sequence();

        s.AppendCallback(() => { ChangePhysic(false); });
        s.Append(transform.DOScale(0, .5f));
        s.AppendCallback(() => { ChangePhysic(true); OnDisabled?.Invoke(this); });
    }

    public void Click()
    {
        OnClicked?.Invoke(this);
    }

    private void ChangePhysic(bool state)
    {
        if (state)
        {
            _rb.bodyType = RigidbodyType2D.Dynamic;
            _rb.simulated = true;
        }
        else
        {
            _rb.velocity = Vector2.zero;
            _rb.bodyType = RigidbodyType2D.Kinematic;
            _rb.simulated = false;
        }
    }
}
