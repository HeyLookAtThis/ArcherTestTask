using Spine;
using Spine.Unity;
using UnityEngine;

[RequireComponent(typeof(BoneFollower))]
public class Bow : MonoBehaviour
{
    [SerializeField] private float _shootForse;
    [SerializeField] private Arrow _arrowPrefab;
    [SerializeField] private Target _target;
    [SerializeField] private AnimationsPlayer _animationsPlayer;

    private BoneFollower _boneFollower;
    private Arrow _arrow;

    private void Awake()
    {
        _boneFollower = GetComponent<BoneFollower>();
    }

    private void OnEnable()
    {
        _animationsPlayer.State.Event += OnShoot;
    }

    private void OnDisable()
    {
        _animationsPlayer.State.Event -= OnShoot;
    }

    private void Start()
    {
        _boneFollower.followXYPosition = true;
        _boneFollower.followBoneRotation = true;

        _arrow = Instantiate(_arrowPrefab, transform);
    }

    private void OnShoot(TrackEntry trackEntry, Spine.Event shoot)
    {
        if (shoot.Data == _animationsPlayer.TargetEventData)
            if (!_arrow.IsFlying)
                _arrow.TakeFlight(transform.position, _target.TopTransform, _shootForse);
    }
}
