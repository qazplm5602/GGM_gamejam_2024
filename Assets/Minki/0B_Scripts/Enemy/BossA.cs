using System.Collections;
using UnityEngine;

public class BossA : MonoBehaviour
{
    private Transform _playerTrm;
    private EnemyController _controller;

    private void Awake() {
        _controller = GetComponent<EnemyController>();

        _playerTrm = GameManager.Instance.playerTrm;
    }

    private void Start() {
        InvokeRepeating("Dash", 1f, 2f);
    }

    private void Dash() {
        StartCoroutine(DashRoutine());
    }

    private IEnumerator DashRoutine() {
        _controller.moveable = false;

        Vector3 direction = _playerTrm.position - transform.position;

        float time = 0f;
        while(time < 0.3f) {
            time += Time.deltaTime;
            yield return null;
        }
        while(time < 0.6f) {
            transform.position += direction.normalized * 20f * Time.deltaTime;
            time += Time.deltaTime;
            yield return null;
        }
        
        _controller.moveable = true;
    }
}
