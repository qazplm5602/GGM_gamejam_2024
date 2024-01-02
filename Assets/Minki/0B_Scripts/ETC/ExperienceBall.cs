using UnityEngine;

public class ExperienceBall : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private void Awake() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable() {
        _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.b, Random.Range(0, 256));
    }
}
