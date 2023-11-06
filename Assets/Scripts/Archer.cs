using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Archer : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private UnityAction _clickedDown;
    private UnityAction _clickedUp;

    public event UnityAction ClickedDown
    {
        add => _clickedDown += value;
        remove => _clickedDown -= value;
    }

    public event UnityAction ClickedUp
    {
        add => _clickedUp += value;
        remove => _clickedUp -= value;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _clickedDown?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _clickedUp?.Invoke();
    }
}
