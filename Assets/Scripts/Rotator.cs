using Spine;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] float _angleMultiplier;
    [SerializeField] private AnimationsPlayer _animationsPlayer;
    [SerializeField] private MousePositionReader _mousePositionReader;

    private float _minZRotationOffset;
    private float _maxZRotationOffcet;
    private float _zAxisAngle;

    private void Awake()
    {
        _minZRotationOffset = 0;
        _maxZRotationOffcet = 0.54f;
        _zAxisAngle = 65;
    }

    private void OnEnable()
    {
        _animationsPlayer.State.Event += OnResetRotation;
        _mousePositionReader.PositionChanged += OnRotate;
    }

    private void OnDisable()
    {
        _animationsPlayer.State.Event -= OnResetRotation;
        _mousePositionReader.PositionChanged -= OnRotate;
    }

    private void OnRotate(Vector2 mousePositionShift)
    {
        transform.Rotate(Vector3.forward, mousePositionShift.y * _angleMultiplier);
        TryCorrectRotation();
    }

    private void OnResetRotation(TrackEntry trackEntry, Spine.Event shoot)
    {
        if (shoot.Data == _animationsPlayer.TargetEventData)
            transform.rotation = Quaternion.identity;
    }

    private void TryCorrectRotation()
    {
        if (transform.rotation.z > _maxZRotationOffcet)
            transform.rotation = Quaternion.AngleAxis(_zAxisAngle, Vector3.forward);
        else if (transform.rotation.z < _minZRotationOffset)
            transform.rotation = Quaternion.AngleAxis(_minZRotationOffset, Vector3.forward);
    }
}
