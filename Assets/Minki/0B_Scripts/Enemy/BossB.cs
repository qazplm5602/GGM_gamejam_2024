using System.Collections;
using Unity.VisualScripting.FullSerializer.Internal;
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
        StartCoroutine(Pattern2());
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

    private IEnumerator Pattern2() {
        yield return new WaitForSeconds(2f);
        
        _controller.moveable = false;

        for(int i = 0; i < 5; ++i) {
            float angle = Random.Range(0f, 360f);
            Vector3 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
            PoolManager.Instance.Pop("BossProjectile", _playerTrm.position + direction.normalized * 3, angle + 180);
            yield return new WaitForSeconds(0.07f);
        }

        yield return new WaitForSeconds(0.1f);
        _controller.moveable = true;
    }
}
