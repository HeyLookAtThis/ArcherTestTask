using DG.Tweening;
using Spine.Unity;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryLine : MonoBehaviour
{
    [SerializeField] private int _pointsCount;
    [SerializeField] private float _distanceScaleMultiplier;
    [SerializeField] private MousePositionReader _mousePositionReader;
    [SerializeField] private TrajectoryPoint _point;
    [SerializeField] private Archer _archer;
    [SerializeField] private Target _target;
    
    private List<TrajectoryPoint> _trajectoryLine;

    private void Awake()
    {
        InstantiateTrajectoryLine();
    }

    private void OnEnable()
    {
        _archer.ClickedDown += OnChangeVisible;
        _archer.ClickedUp += OnChangeVisible;
    }

    private void OnDisable()
    {
        _archer.ClickedDown -= OnChangeVisible;
        _archer.ClickedUp -= OnChangeVisible;
    }

    private void Update()
    {
        if (!_mousePositionReader.IsWorking)
            return;

        SetLength();
    }

    private void InstantiateTrajectoryLine()
    {
        _trajectoryLine = new List<TrajectoryPoint>();

        for (int i = 0; i < _pointsCount; i++)
        {
            TrajectoryPoint point = Instantiate(_point, transform);
            point.SetScale(i * _distanceScaleMultiplier);
            _trajectoryLine.Add(point);
        }
    }

    private void SetLength()
    {
        for (int i = 0; i < _pointsCount; i++)
            _trajectoryLine[i].transform.position = SetPointCoordinate(i + 1);
    }

    private Vector2 SetPointCoordinate(int index)
    {
        float wayPercentage = 0.5f;
        float time = (float)index / _pointsCount * wayPercentage;
        Vector2 position = transform.position;

        return Mathf.Pow(1 - time, 2) * position + 2 * time * (1 - time) * _target.TopPosition + Mathf.Pow(time, 2) * _target.BottomPosition;
    }

    private void OnChangeVisible()
    {
        foreach (var point in _trajectoryLine)
            point.ChangeVisible();
    }
}
