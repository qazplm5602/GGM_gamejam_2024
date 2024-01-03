using UnityEngine;

public class ExperienceBall : MonoBehaviour
{
    [SerializeField] private bool _boss = false;
    private SpriteRenderer _spriteRenderer;

    private void Awake() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable() {
        _spriteRenderer.color = _boss ? new Color(1, Random.Range(0f, 1f), 0) : new Color(0, 1, Random.Range(0f, 1f));
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
        GameManager.Instance.player.ExpUP(_boss ? 300 : 30);
        PoolManager.Instance.Push("Exp", gameObject);
    }
}
