using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private float _timer = 0f;

    private void Update() {
        _timer += Time.deltaTime;

        if(_timer >= 5f) {
            _timer = 0f;
            GameManager.Instance.SetHP(++GameManager.Instance.curHp);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer == 8) {
            GameManager.Instance.curHp -= 15;
            GameManager.Instance.SetHP(GameManager.Instance.curHp);
        }
    }
}
