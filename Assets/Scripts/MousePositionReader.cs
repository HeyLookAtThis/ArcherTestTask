using UnityEngine;
using UnityEngine.Events;

public class MousePositionReader : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Archer _archer;

    private bool _isWorking;
    private bool _included;

    private Vector2? _previousWorldPosition;
    private Vector2 _currentWorldPosition;

    private UnityAction<Vector2> _positionChanged;

    public event UnityAction<Vector2> PositionChanged
    {
        add => _positionChanged += value;
        remove => _positionChanged -= value;
    }

    public bool IsWorking => _isWorking;

    private void OnEnable()
    {
        _archer.ClickedDown += OnTurnOn;
        _archer.ClickedUp += OnTurnOff;
    }

    private void OnDisable()
    {
        _archer.ClickedDown -= OnTurnOn;
        _archer.ClickedUp -= OnTurnOff;
    }

    private void Update()
    {
        if (!_included)
            return;

        var mousePosition = Input.mousePosition;
        _currentWorldPosition = _camera.ScreenToWorldPoint(mousePosition);

        if (!_previousWorldPosition.HasValue)
            _previousWorldPosition = _currentWorldPosition;

        if (!_isWorking)
            _isWorking = true;

        if (_currentWorldPosition != _previousWorldPosition.Value)
            _positionChanged?.Invoke(_previousWorldPosition.Value - _currentWorldPosition);

        _previousWorldPosition = _currentWorldPosition;
    }

    private void OnTurnOn()
    {
        _included = true;
    }

    private void OnTurnOff()
    {
        _included = false;
        _isWorking = false;
        _previousWorldPosition = null;
    }
}
