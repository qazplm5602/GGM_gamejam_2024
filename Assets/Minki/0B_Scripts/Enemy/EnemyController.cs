using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [HideInInspector] public bool moveable = true;
    [HideInInspector] public bool freezeFlip = false;

    [SerializeField] private EnemySO _enemySO;
    [SerializeField] private bool _boss = false;
    [SerializeField] private bool _bossE = false;

    private int _hp;
    private int _damage;
    private float _speed;
    private bool _invincibility = false;
    private Transform _playerTrm;
    private Rigidbody2D _rigidbody;

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        _hp = _enemySO.hp;
        _damage = _enemySO.damage;
        _speed = _enemySO.speed;

        _playerTrm = GameManager.Instance.playerTrm;
    }
    
    private void OnEnable() {
        _hp = _enemySO.hp;
        StopAllCoroutines();
        moveable = true;
        freezeFlip = false;
        _invincibility = false;
        if(_boss) return;

        GetComponent<Animator>().runtimeAnimatorController = GameManager.Instance.enemy[Random.Range(0, GameManager.Instance.enemy.Length)];
    }

    private void Update() {
        if(freezeFlip) return;

        if(_playerTrm.position.x < transform.position.x) transform.localScale = new Vector3(-1, 1, 1);
        else if(_playerTrm.position.x > transform.position.x) transform.localScale = new Vector3(1, 1, 1);
    }

    private void FixedUpdate() {
        Move();
    }

    private void Move() {
        if(moveable) {
            Vector3 direction = GameManager.Instance.playerTrm.position - transform.position;

            transform.position += direction.normalized * _speed * Time.deltaTime;
        }
    }

    public void Hit(int damage, Vector3 position) {
        if(_invincibility) return;

        _hp -= damage;
        StartCoroutine(Knockback(transform.position - position));

        if(_hp <= 0) {
            if(_bossE) {
                _bossE = false;
                StartCoroutine(GetComponent<BossE>().Transform());
            }
            else {
                GameManager.Instance.enemyKill++;
                if(TryGetComponent(out BossE boss)) {
                    boss.Dead();
                }
                else {
                    if(_boss) {
                        PoolManager.Instance.Pop("BossExp", transform.position);
                        Destroy(gameObject);
                    }
                    else {
                        PoolManager.Instance.Pop("Exp", transform.position);
                        PoolManager.Instance.Push("Enemy", gameObject);
                    }
                }
            }
        }
    }

    private IEnumerator Knockback(Vector2 direction) {
        moveable = false;
        freezeFlip = true;
        _invincibility = true;

        _rigidbody.AddForce(direction.normalized * 2f, ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.1f);

        _invincibility = false;

        yield return new WaitForSeconds(0.5f);

        moveable = true;
        freezeFlip = false;
    }
}
