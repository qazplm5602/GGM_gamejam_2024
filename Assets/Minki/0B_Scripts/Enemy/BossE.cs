using System.Collections;
using UnityEngine;

public class BossE : MonoBehaviour
{
    [SerializeField] private float _patternDelay = 5f;
    [SerializeField] private Transform _castTrm;

    private bool _freezeFlip = false;
    private Transform _playerTrm;
    private Animator _animator;
    private EnemyController _controller;

    private void Awake() {
        _controller = GetComponent<EnemyController>();
        _animator = transform.GetChild(0).GetComponent<Animator>();

        _playerTrm = GameManager.Instance.playerTrm;
    }

    private void Update() {
        if(_freezeFlip) return;

        if(_playerTrm.position.x > transform.position.x) transform.localScale = new Vector3(-1, 1, 1);
        else if(_playerTrm.position.x < transform.position.x) transform.localScale = new Vector3(1, 1, 1);
    }

    public IEnumerator Transform() {
        _controller.moveable = false;
        _freezeFlip = true;
        _animator.SetTrigger("transform");

        yield return new WaitForSeconds(3.2f);

        _controller.moveable = true;
        _freezeFlip = false;

        StartCoroutine(RandomPattern());
    }

    private IEnumerator RandomPattern() {
        yield return new WaitForSeconds(1f);

        while(true) {
            switch(Random.Range(0, 3)) {
                case 0: StartCoroutine(Pattern1()); break;
                case 1: StartCoroutine(Pattern2()); break;
                case 2: StartCoroutine(Pattern3()); break;
            }
            yield return new WaitForSeconds(_patternDelay);
        }
    }

    private IEnumerator Pattern1() {
        _controller.moveable = false;
        _freezeFlip = true;
        _animator.SetBool("cast", true);

        for(int i = 0; i < 3; ++i) {
            for(int j = 0; j < 3; ++j) {
                PoolManager.Instance.Pop("BoomE", _playerTrm.position);
                yield return new WaitForSeconds(0.2f);
            }
            yield return new WaitForSeconds(0.5f);
        }

        _controller.moveable = true;
        _freezeFlip = false;
        _animator.SetBool("cast", false);
    }

    private IEnumerator Pattern2() {
        _controller.moveable = false;
        _freezeFlip = true;
        _animator.SetBool("cast", true);

        yield return new WaitForSeconds(0.5f);
        for(int j = 0; j < 4; ++j) {
            Vector2 direction = _playerTrm.position - _castTrm.position;
            direction.Normalize();
            PoolManager.Instance.Pop("Fireball", _castTrm.position, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
            yield return new WaitForSeconds(0.2f);
        }

        _controller.moveable = true;
        _freezeFlip = false;
        _animator.SetBool("cast", false);
    }

    private IEnumerator Pattern3() {
        _controller.moveable = false;
        _freezeFlip = true;
        _animator.SetTrigger("fire");
        
        yield return new WaitForSeconds(1.2f);

        float range = 1f;
        Vector3 center = _playerTrm.position;
        for(int i = 0; i < 3; ++i) {
            int startAngle = Random.Range(0, 360);
            range += 2.3f;
            for(int j = 0; j < 33 * (i + 1); ++j) {
                float angle = startAngle + j * 10f / (i + 1);
                Vector3 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
                PoolManager.Instance.Pop("Fire", center + direction.normalized * range).GetComponent<Fire>().center = center;
            }
            yield return new WaitForSeconds(0.1f);
        }
        
        _controller.moveable = true;
        _freezeFlip = false;
    }
}
