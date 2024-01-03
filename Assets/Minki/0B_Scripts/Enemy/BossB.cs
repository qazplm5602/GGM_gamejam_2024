using System.Collections;
using UnityEngine;

public class BossB : MonoBehaviour
{
    [SerializeField] private float _range = 3f;

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
        InvokeRepeating("RandomPattern", 1f, 2f);
    }

    private void Update() {
        if(_freezeFlip) return;

        if(_playerTrm.position.x > transform.position.x) transform.localScale = new Vector3(-1, 1, 1);
        else if(_playerTrm.position.x < transform.position.x) transform.localScale = new Vector3(1, 1, 1);
    }

    private void RandomPattern() {
        StartCoroutine(Random.Range(0, 2) == 0 ? Pattern1() : Pattern2());
    }

    private IEnumerator Pattern1() {
        _controller.moveable = false;
        _freezeFlip = true;
        
        _animator.SetTrigger("attack");
        int number = Random.Range(4, 9);
        for(int i = 0; i < number; ++i) {
            float angle = i / (float)number * 360f;
            Vector3 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
            PoolManager.Instance.Pop("BossProjectile", _playerTrm.position + direction.normalized * _range, angle + 180);
        }

        yield return new WaitForSeconds(0.35f);

        _controller.moveable = true;
        _freezeFlip = false;
    }

    private IEnumerator Pattern2() {
        _controller.moveable = false;
        _freezeFlip = true;

        _animator.SetTrigger("attack");
        for(int i = 0; i < 5; ++i) {
            float angle = Random.Range(0f, 360f);
            Vector3 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
            PoolManager.Instance.Pop("BossProjectile", _playerTrm.position + direction.normalized * _range, angle + 180);
        }

        yield return new WaitForSeconds(0.35f);
        _controller.moveable = true;
        _freezeFlip = false;
    }
}
