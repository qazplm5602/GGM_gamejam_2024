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
    [SerializeField] private EnemySO _enemySO;

    private int _hp;
    private int _damage;
    private float _speed;

    private void Start() {
        _hp = _enemySO.hp;
        _damage = _enemySO.damage;
        _speed = _enemySO.speed;
    }

    private void Update() {
        Vector3 direction = GameManager.Instance.playerTrm.position - transform.position;

        transform.position += direction.normalized * _speed * Time.deltaTime;
    }
}
