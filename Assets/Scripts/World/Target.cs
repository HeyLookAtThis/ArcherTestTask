using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Archer _archer;
    [SerializeField] private MousePositionReader _mousePositionReader;
    [SerializeField] private GameObject _topPoint;
    [SerializeField] private GameObject _bottomPoint;

    private Coroutine _topPointMover;

    private Vector2 _startingTopPosition;
    private Vector2 _startingBottomPosition;

    private Vector2 _mousePosition;

    private float _minXAxisCoordinate;
    private float _maxXAxisCoordinate;

    private float _minYAxisTopPointCoordinate;
    private float _maxYAxisTopPointCoordinate;

    private float _groundYAxisCoordinate;

    public Vector2 TopPosition => _topPoint.transform.position;

    public Vector2 BottomPosition => _bottomPoint.transform.position;

    public Transform TopTransform => _topPoint.transform;

    private void Awake()
    {
        _maxYAxisTopPointCoordinate = 5f;
        _minYAxisTopPointCoordinate = -1.3f;

        _minXAxisCoordinate = -2f;
        _maxXAxisCoordinate = 10f;

        _groundYAxisCoordinate = -3f;

        _startingTopPosition = new Vector2(_minXAxisCoordinate, _minYAxisTopPointCoordinate);
        _startingBottomPosition = new Vector2(_minXAxisCoordinate, _groundYAxisCoordinate);
    }

    private void OnEnable()
    {
        _archer.ClickedDown += OnSetStartinPosition;
        _archer.ClickedUp += OnStartTopPointMove;
        _mousePositionReader.PositionChanged += OnShiftTargetPoints;
    }

    private void OnDisable()
    {
        _archer.ClickedDown -= OnSetStartinPosition;
        _archer.ClickedUp -= OnStartTopPointMove;
        _mousePositionReader.PositionChanged -= OnShiftTargetPoints;
    }

    private void OnSetStartinPosition()
    {
        _topPoint.transform.position = _startingTopPosition;
        _bottomPoint.transform.position = _startingBottomPosition;
        _mousePosition = new Vector2(_groundYAxisCoordinate, _minYAxisTopPointCoordinate);
    }

    private void OnShiftTargetPoints(Vector2 shiftMousePosition)
    {
        _mousePosition += shiftMousePosition;

        ShiftButtonPoint();
        ShiftTopPoint();
    }

    private void ShiftTopPoint()
    {
        _mousePosition.y = GetCorrectCoordinate(_mousePosition.y, _minYAxisTopPointCoordinate, _maxYAxisTopPointCoordinate);
        _topPoint.transform.DOMoveY(_mousePosition.y, Time.deltaTime);
    }

    private void ShiftButtonPoint()
    {
        _mousePosition.x = GetCorrectCoordinate(_mousePosition.x, _minXAxisCoordinate, _maxXAxisCoordinate);
        _bottomPoint.transform.DOMoveX(_mousePosition.x, Time.deltaTime);
    }

    private float GetCorrectCoordinate(float coordinate, float startingCoordinate, float endingCoordinate)
    {
        if (coordinate > endingCoordinate)
            coordinate = endingCoordinate;
        else if (coordinate < startingCoordinate)
            coordinate = startingCoordinate;

        return coordinate;
    }

    private void OnStartTopPointMove()
    {
        if(_topPointMover != null)
            StopCoroutine(_topPointMover);

        _topPointMover = StartCoroutine(TopPointMover());
    }

    private IEnumerator TopPointMover()
    {
        var waitTime = new WaitForEndOfFrame();

        while(_topPoint.transform.position != _bottomPoint.transform.position)
        {
            _topPoint.transform.position = Vector3.MoveTowards(_topPoint.transform.position, _bottomPoint.transform.position, _speed * Time.deltaTime);
            yield return waitTime;
        }

        if (_topPoint.transform.position == _bottomPoint.transform.position)
            yield break;
    }
}