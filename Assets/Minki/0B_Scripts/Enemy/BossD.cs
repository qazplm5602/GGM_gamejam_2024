using System.Collections;
using UnityEngine;

public class BossD : MonoBehaviour
{
    [SerializeField] private float _radius = 5f;

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

            PoolManager.Instance.Pop("Boom", _playerTrm.position);

            yield return new WaitForSeconds(1f);

            _controller.moveable = true;

            yield return new WaitForSeconds(0.5f);
        }
    }
}
