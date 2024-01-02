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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            PushEXP();
        }
    }
    public void PushEXP()
    {
        print("¾Æ´Ï");
        GameManager.Instance.player.ExpUP();
        PoolManager.Instance.Push("EXP", gameObject);
    }
}
