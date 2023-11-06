using Spine.Unity;
using UnityEngine;

[RequireComponent(typeof(BoneFollower))]
public class Bow : MonoBehaviour
{
    [SerializeField] private float _shootForse;
    [SerializeField] private Arrow _arrowPrefab;
    [SerializeField] private Target _target;
    [SerializeField] private Archer _archer;
    [SerializeField] private Transform _quiver;

    private BoneFollower _boneFollower;
    private Arrow _arrow;

    private void Awake()
    {
        _boneFollower = GetComponent<BoneFollower>();
    }

    private void OnEnable()
    {
        _archer.ClickedUp += OnShoot;
    }

    private void OnDisable()
    {
        _archer.ClickedUp -= OnShoot;
    }

    private void Start()
    {
        _boneFollower.followXYPosition = true;
        _boneFollower.followBoneRotation = true;

        _arrow = Instantiate(_arrowPrefab, _quiver);
    }

    private void OnShoot()
    {
        if (!_arrow.IsFlying)
            _arrow.TakeFlight(transform.position, _target.TopTransform, _shootForse);
    }
}
