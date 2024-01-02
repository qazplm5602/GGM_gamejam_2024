using UnityEngine;

public enum EnemyType { Combat = 0, Projectile }

[CreateAssetMenu(menuName = "SO/EnemySO")]
public class EnemySO : ScriptableObject
{
    public int hp = 10;
    public int damage = 2;
    public float speed = 2;
    public EnemyType enemyType;
}

public class EnemyController : MonoBehaviour
{
    public bool moveable = true;

    [SerializeField] private EnemySO _enemySO;

    private int _hp;
    private int _damage;
    private float _speed;
    private EnemyType _enemyType;
    private Transform _playerTrm;

    private void Start() {
        _hp = _enemySO.hp;
        _damage = _enemySO.damage;
        _speed = _enemySO.speed;
        _enemyType = _enemySO.enemyType;

        _playerTrm = GameManager.Instance.playerTrm;
    }

    private void Update() {
        Move();
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
            PoolManager.Instance.Pop("Exp", transform.position);
            PoolManager.Instance.Push("Enemy", gameObject);
        }
    }
}
