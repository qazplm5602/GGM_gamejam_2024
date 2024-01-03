using System.Collections;
using UnityEngine;

public class BossA : MonoBehaviour
{
    [SerializeField] private float _delay = 0.5f;
    [SerializeField] private float _dashTime = 0.6f;

    private bool _freezeFlip = false;
    private Transform _playerTrm;
    private Animator _animator;
    private EnemyController _controller;

    private void Awake() {
        _controller = GetComponent<EnemyController>();
        _animator = GetComponent<Animator>();

        _playerTrm = GameManager.Instance.playerTrm;
    }

    private void Start() {
        InvokeRepeating("Dash", 1f, 2f);
        _animator.SetBool("isMove", true);
    }

    private void Update() {
        if(_freezeFlip) return;

        if(_playerTrm.position.x > transform.position.x) transform.localScale = new Vector3(-1, 1, 1);
        else if(_playerTrm.position.x < transform.position.x) transform.localScale = new Vector3(1, 1, 1);
    }

    private void Dash() {
        StartCoroutine(DashRoutine());
    }

    private IEnumerator DashRoutine() {
        _controller.moveable = false;
        _animator.SetBool("isMove", false);
        _freezeFlip = true;

        Vector3 direction = _playerTrm.position - transform.position;

        float time = 0f;
        while(time < _delay) {
            time += Time.deltaTime;
            yield return null;
        }
        _animator.SetBool("isMove", true);
        _freezeFlip = false;
        while(time < _delay + _dashTime) {
            transform.position += direction.normalized * 20f * Time.deltaTime;
            time += Time.deltaTime;
            yield return null;
        }
        
        _controller.moveable = true;
    }
}
