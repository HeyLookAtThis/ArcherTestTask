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
        transform.LookAt(_target);

        if (transform.position == _target.position)
        {
            ChangeVisible();
            _isFlying = false;
        }
    }

    public void TakeFlight(Vector2 position, Transform target, float speed)
    {
        transform.position = position;

        if (!_isFlying)
            _isFlying = true;

        _target = target;
        _speed = speed;

        ChangeVisible();
    }
}
