using System.Collections;
using UnityEngine;

public class BossC : MonoBehaviour
{
    [SerializeField] private float _radius = 5f;
    [SerializeField] private float _power = 10f;

    private Transform _playerTrm;
    private EnemyController _controller;

    private void Awake() {
        _controller = GetComponent<EnemyController>();

        _playerTrm = GameManager.Instance.playerTrm;
    }

    private void Start() {
        StartCoroutine(SpawnBoom());
    }

    private IEnumerator SpawnBoom() {
        while(true) {
            yield return new WaitUntil(() => Vector2.Distance(transform.position, _playerTrm.position) < _radius);

            _controller.moveable = false;

            GameObject obj = PoolManager.Instance.Pop("BoomC", transform.position);
            Vector2 direction = _playerTrm.position - transform.position;
            obj.GetComponent<Rigidbody2D>().AddForce(direction.normalized * _power, ForceMode2D.Impulse);

            yield return new WaitForSeconds(0.7f);

            _controller.moveable = true;

            yield return new WaitForSeconds(0.8f);
        }
    }
}
