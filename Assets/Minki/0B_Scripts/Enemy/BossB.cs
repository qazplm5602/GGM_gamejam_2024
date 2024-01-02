using System.Collections;
using UnityEngine;

public class BossB : MonoBehaviour
{
    private Transform _playerTrm;
    private EnemyController _controller;

    private void Awake() {
        _controller = GetComponent<EnemyController>();

        _playerTrm = GameManager.Instance.playerTrm;
    }

    private void Start() {
        StartCoroutine(Pattern1());
    }

    private IEnumerator Pattern1() {
        yield return new WaitForSeconds(2f);
        _controller.moveable = false;
        
        float range = 3f;
        PoolManager.Instance.Pop("BossProjectile", _playerTrm.position + Vector3.up * range , -90);
        PoolManager.Instance.Pop("BossProjectile", _playerTrm.position + Vector3.down * range, 90);
        PoolManager.Instance.Pop("BossProjectile", _playerTrm.position + Vector3.right * range, 180);
        PoolManager.Instance.Pop("BossProjectile", _playerTrm.position + Vector3.left * range, 0);

        yield return new WaitForSeconds(0.35f);

        _controller.moveable = true;
    }
}
