using UnityEngine;

public class SpriteObject : MonoBehaviour
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer.sprite = _sprite;
    }

    private void Start()
    {
        ChangeVisible();
    }

    public void ChangeVisible()
    {
        if (!_spriteRenderer.enabled)
            _spriteRenderer.enabled = true;
        else
            _spriteRenderer.enabled = false;
    }
}
