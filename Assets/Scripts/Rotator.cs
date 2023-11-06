using Spine;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] float _angleMultiplier;
    [SerializeField] private AnimationsPlayer _animationsPlayer;
    [SerializeField] private MousePositionReader _mousePositionReader;

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
    }

    private void OnResetRotation(TrackEntry trackEntry, Spine.Event shoot)
    {
        if (shoot.Data == _animationsPlayer.TargetEventData)
            transform.rotation = Quaternion.identity;
    }
}
