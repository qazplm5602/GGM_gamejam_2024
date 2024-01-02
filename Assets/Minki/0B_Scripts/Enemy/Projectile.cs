using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _lifeTime = 1f;
    [SerializeField] private float _delayTime = 0.5f;

    private float _timer = 0f;

    private void Update() {
        _timer += Time.deltaTime;
        if(_timer < _delayTime) return;
        
        transform.Translate(Vector3.right * _speed * Time.deltaTime);

        if(_timer >= _lifeTime + _delayTime) PoolManager.Instance.Push("BossProjectile", gameObject);
    }
}
