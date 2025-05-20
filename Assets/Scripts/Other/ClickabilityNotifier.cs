using UnityEngine;
using UnityEngine.Events;

public class ClickabilityNotifier : MonoBehaviour
{
    public UnityEvent OnClick = new UnityEvent();

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null && hit.collider.transform.IsChildOf(transform))
            {
                OnClick?.Invoke();
            }
        }
    }
}
