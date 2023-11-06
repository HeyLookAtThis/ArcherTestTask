using DG.Tweening;
using UnityEngine;

public class Arrow : SpriteObject
{
    private Transform _target;
    private bool _isFlying;
    private float _speed;

    public bool IsFlying => _isFlying;

    private void Update()
    {
        if (!_isFlying)
            return;

        transform.position = Vector2.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
        transform.DOLookAt(_target.position, Time.deltaTime);

        if (transform.position == _target.position)
            TurnOff();
    }

    public void TakeFlight(Vector2 position, Transform target, float speed)
    {
        transform.position = position;
        _target = target;
        _speed = speed;

        if (!_isFlying)
            _isFlying = true;

        ChangeVisible();
    }

    private void TurnOff()
    {
        ChangeVisible();
        _isFlying = false;
    }
}
