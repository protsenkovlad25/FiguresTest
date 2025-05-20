using UnityEngine;

public enum ScreenEdge
{
    Left, Right, Top, Bottom
}

public class Border : MonoBehaviour
{
    [SerializeField] private ScreenEdge _edge;
    [SerializeField] private float _thickness = 1f;
    [SerializeField] private float _offset = 0f;

    private void Awake()
    {
        SetPosition();
    }
    private void OnValidate()
    {
        SetPosition();
    }

    public void Init()
    {
        SetPosition();
    }

    private void SetPosition()
    {
        Camera cam = Camera.main;

        Vector3 newPosition = transform.position;
        Vector3 newScale = transform.localScale;

        float screenHeight = cam.orthographicSize * 2;
        float screenWidth = screenHeight * cam.aspect;

        switch (_edge)
        {
            case ScreenEdge.Left:
                newPosition = cam.ViewportToWorldPoint(new Vector3(0 + _offset, 0.5f, cam.nearClipPlane));
                newPosition.x -= _thickness / 2;
                newScale = new Vector3(_thickness, screenHeight, 1);
                break;

            case ScreenEdge.Right:
                newPosition = cam.ViewportToWorldPoint(new Vector3(1 - _offset, 0.5f, cam.nearClipPlane));
                newPosition.x += _thickness / 2;
                newScale = new Vector3(_thickness, screenHeight, 1);
                break;

            case ScreenEdge.Top:
                newPosition = cam.ViewportToWorldPoint(new Vector3(0.5f, 1 - _offset, cam.nearClipPlane));
                newPosition.y += _thickness / 2;
                newScale = new Vector3(screenWidth, _thickness, 1);
                break;

            case ScreenEdge.Bottom:
                newPosition = cam.ViewportToWorldPoint(new Vector3(0.5f, 0 + _offset, cam.nearClipPlane));
                newPosition.y -= _thickness / 2;
                newScale = new Vector3(screenWidth, _thickness, 1);
                break;
        }

        newPosition.z = 0;

        transform.position = newPosition;
        transform.localScale = newScale;
    }
}
