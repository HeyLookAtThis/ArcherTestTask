using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public abstract class SpriteObject : MonoBehaviour
{
    [SerializeField] private Sprite _sprite;

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _sprite;
    }

    private void Start()
    {
        ChangeVisible();
    }

    public void ChangeVisible()
    {
        if (_spriteRenderer.enabled == false)
            _spriteRenderer.enabled = true;
        else
            _spriteRenderer.enabled = false;
    }
}
