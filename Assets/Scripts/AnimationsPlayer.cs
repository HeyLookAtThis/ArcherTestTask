using Spine;
using Spine.Unity;
using UnityEngine;

[RequireComponent(typeof(SkeletonAnimation), typeof(Archer))]
public class AnimationsPlayer : MonoBehaviour
{
    private const string _EventName = "shoot";

    [SerializeField] private AnimationReferenceAsset _idle;
    [SerializeField] private AnimationReferenceAsset _attack_start;
    [SerializeField] private AnimationReferenceAsset _attack_target;
    [SerializeField] private AnimationReferenceAsset _attack_finish;

    private SkeletonAnimation _skeletonAnimation;
    private EventData _targetEventData;
    private Archer _archer;

    private bool _isLooping;
    private int _trackIndex;
    private float _delay;

    public Spine.AnimationState State => _skeletonAnimation.state;

    public EventData TargetEventData => _targetEventData;

    private void Awake()
    {
        _skeletonAnimation = GetComponent<SkeletonAnimation>();
        _archer = GetComponent<Archer>();

        _targetEventData = _skeletonAnimation.Skeleton.Data.FindEvent(_EventName);

        _trackIndex = 0;
        _delay = 0.5f;

        _isLooping = true;
        SetAnimation(_idle, _isLooping);
    }

    private void OnEnable()
    {
        _archer.ClickedDown += OnStartPlayingShot;
        _archer.ClickedUp += OnFinishPlayingShot;
    }

    private void OnDisable()
    {
        _archer.ClickedDown -= OnStartPlayingShot;
        _archer.ClickedUp -= OnFinishPlayingShot;
    }

    private void OnStartPlayingShot()
    {
        _isLooping = false;

        SetAnimation(_attack_start, _isLooping);
        AddAnimation(_attack_target, _isLooping);
    }

    private void OnFinishPlayingShot()
    {
        _isLooping = false;
        SetAnimation(_attack_finish, _isLooping);

        _isLooping = true;
        AddAnimation(_idle, _isLooping);
    }

    private void SetAnimation(AnimationReferenceAsset skeletonAnimation, bool loop)
    {
        _skeletonAnimation.state.SetAnimation(_trackIndex, skeletonAnimation, loop);
    }

    private void AddAnimation(AnimationReferenceAsset animationReferenceAsset, bool loop)
    {
        _skeletonAnimation.state.AddAnimation(_trackIndex, animationReferenceAsset, loop, _delay);
    }
}
