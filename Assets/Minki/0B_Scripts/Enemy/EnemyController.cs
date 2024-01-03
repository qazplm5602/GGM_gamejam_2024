using UnityEngine;

[CreateAssetMenu(menuName = "SO/EnemySO")]
public class EnemySO : ScriptableObject
{
    public int hp = 10;
    public int damage = 2;
    public float speed = 2;
}

public class EnemyController : MonoBehaviour
{
    [HideInInspector] public bool moveable = true;
    [HideInInspector] public bool freezeFlip = false;

    [SerializeField] private EnemySO _enemySO;
    [SerializeField] private bool _bossE = false;

    private int _hp;
    private int _damage;
    private float _speed;
    private Transform _playerTrm;

    private void Start() {
        _hp = _enemySO.hp;
        _damage = _enemySO.damage;
        _speed = _enemySO.speed;

        _playerTrm = GameManager.Instance.playerTrm;
    }
    
    private void OnEnable() {
        GetComponent<Animator>().runtimeAnimatorController = GameManager.Instance.enemy[Random.Range(0, GameManager.Instance.enemy.Length)];
    }

    private void Update() {
        Move();

        if(Input.GetKeyDown(KeyCode.L)) Hit(100);
        
        if(freezeFlip) return;

        if(_playerTrm.position.x > transform.position.x) transform.localScale = new Vector3(-1, 1, 1);
        else if(_playerTrm.position.x < transform.position.x) transform.localScale = new Vector3(1, 1, 1);
    }

    private void Move() {
        if(moveable) {
            Vector3 direction = GameManager.Instance.playerTrm.position - transform.position;

            transform.position += direction.normalized * _speed * Time.deltaTime;
        }
    }

    public void Hit(int damage) {
        _hp -= damage;

        if(_hp <= 0) {
            if(_bossE) {
                _bossE = false;
                StartCoroutine(GetComponent<BossE>().Transform());
            }
            else {
                if(TryGetComponent(out BossE boss)) {
                    boss.Dead();
                }
                else {
                    PoolManager.Instance.Pop("Exp", transform.position);
                    PoolManager.Instance.Push("Enemy", gameObject);
                }
            }
        }
    }
}
