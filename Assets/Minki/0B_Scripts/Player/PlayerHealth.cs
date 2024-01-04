using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private float _timer = 0f;

    private void Update() {
        _timer += Time.deltaTime;

        if(_timer >= 5f && GameManager.Instance.curHp < 100) {
            _timer = 0f;
            GameManager.Instance.SetHP(++GameManager.Instance.curHp);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer == 8) {
            GameManager.Instance.curHp -= 15;
            PoolManager.Instance.Pop("PlayerHit", transform.position + new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.5f, -0.1f)));
            GameManager.Instance.SetHP(GameManager.Instance.curHp);
        }
    }
}
