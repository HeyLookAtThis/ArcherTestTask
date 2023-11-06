using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Archer _archer;
    [SerializeField] private MousePositionReader _mousePositionReader;
    [SerializeField] private GameObject _topPoint;
    [SerializeField] private GameObject _bottomPoint;

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

        _minXAxisCoordinate = -4f;
        _maxXAxisCoordinate = 10f;

        _groundYAxisCoordinate = -3f;

        _startingTopPosition = new Vector2(_minXAxisCoordinate, _minYAxisTopPointCoordinate);
        _startingBottomPosition = new Vector2(_minXAxisCoordinate, _groundYAxisCoordinate);
    }

    private void OnEnable()
    {
        _archer.ClickedDown += OnSetStartinPosition;
        _mousePositionReader.PositionChanged += OnMove;
    }

    private void OnDisable()
    {
        _archer.ClickedDown -= OnSetStartinPosition;
        _mousePositionReader.PositionChanged -= OnMove;
    }

    private void OnSetStartinPosition()
    {
        _topPoint.transform.position = _startingTopPosition;
        _bottomPoint.transform.position = _startingBottomPosition;
        _mousePosition = new Vector2(_groundYAxisCoordinate, _minYAxisTopPointCoordinate);
    }

    private void OnMove(Vector2 shiftMousePosition)
    {
        _mousePosition += shiftMousePosition;

        MoveButtonPoint();
        MoveTopPoint();
    }

    private void MoveTopPoint()
    {
        _mousePosition.y = GetCorrectCoordinate(_mousePosition.y, _minYAxisTopPointCoordinate, _maxYAxisTopPointCoordinate);
        _topPoint.transform.DOMove(new Vector2(_bottomPoint.transform.position.x, _mousePosition.y), Time.deltaTime);
    }

    private void MoveButtonPoint()
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
}