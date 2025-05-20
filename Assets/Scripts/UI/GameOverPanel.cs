using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    public event UnityAction OnRestartClick;

    [Header("UI Elements")]
    [SerializeField] private Image _gameOverImage;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Button _restartButton;

    [Header("Sprites")]
    [SerializeField] private Sprite _winSprite;
    [SerializeField] private Sprite _loseSprite;

    [Header("Anim times")]
    [SerializeField] private float _openTime;
    [SerializeField] private float _closeTime;

    private Image _image;
    private float _alpha;

    public void Init()
    {
        _image = GetComponent<Image>();
        _alpha = _image.color.a;
        _image.DOFade(0, 0);

        _gameOverImage.transform.localScale = Vector3.zero;
        _text.transform.localScale = Vector3.zero;
        _restartButton.transform.localScale = Vector3.zero;
    }

    public void Restart()
    {
        OnRestartClick?.Invoke();
    }

    public void Open(bool isWin)
    {
        _text.text = isWin ? "You Win!" : "You Lose :<";
        _gameOverImage.sprite = isWin ? _winSprite : _loseSprite;

        OpenAnim();
    }
    public void Close()
    {
        CloseAnim();
    }

    private void OpenAnim()
    {
        gameObject.SetActive(true);

        Sequence s = DOTween.Sequence();

        s.Append(_image.DOFade(_alpha, _openTime));
        s.Join(_gameOverImage.transform.DOScale(1, _openTime));
        s.Join(_text.transform.DOScale(1, _openTime));
        s.Join(_restartButton.transform.DOScale(1, _openTime));
    }
    private void CloseAnim()
    {
        Sequence s = DOTween.Sequence();

        s.Append(_image.DOFade(0, _closeTime));
        s.Join(_gameOverImage.transform.DOScale(0, _closeTime));
        s.Join(_text.transform.DOScale(0, _closeTime));
        s.Join(_restartButton.transform.DOScale(0, _closeTime));
        s.AppendCallback(() => { gameObject.SetActive(false); });
    }
}
