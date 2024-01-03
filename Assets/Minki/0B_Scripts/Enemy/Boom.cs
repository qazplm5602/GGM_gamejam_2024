using UnityEngine;

public class Boom : MonoBehaviour
{
    [SerializeField] private float _boomDelay;

    private float _timer = 0f;

    private void Update() {
        _timer += Time.deltaTime;

        if(_timer >= _boomDelay) {
            AudioManager.Instance.PlaySound("Dive");
            PoolManager.Instance.Push("Boom", gameObject);
            _timer = 0f;
        }
    }
}
