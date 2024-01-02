using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/PoolSO")]
public class PoolSO : ScriptableObject
{
    public new string name;
    public int count = 10;
    public GameObject prefab;
}

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance = null;

    [SerializeField] private List<PoolSO> _poolList;

    private Dictionary<string, Queue<GameObject>> _poolQueue = new Dictionary<string, Queue<GameObject>>();

    private void Awake() {
        if(Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start() {
        for(int i = 0; i < _poolList.Count; ++i) {
            _poolQueue[_poolList[i].name] = new Queue<GameObject>();
            for(int j = 0; j < _poolList[i].count; ++j) {
                GameObject obj = Instantiate(_poolList[i].prefab, transform.position, Quaternion.identity, transform);
                obj.SetActive(false);
                _poolQueue[_poolList[i].name].Enqueue(obj);
            }
        }
    }

    public GameObject Pop(string name, Vector3 position) {
        GameObject obj = _poolQueue[name].Dequeue();
        obj.transform.parent = null;
        obj.transform.position = position;
        obj.SetActive(true);

        return obj;
    }

    public void Push(string name, GameObject obj) {
        obj.transform.parent = transform;
        obj.transform.position = Vector3.zero;
        obj.transform.rotation = Quaternion.Euler(0, 0, 0);
        obj.SetActive(false);
        _poolQueue[name].Enqueue(obj);
    }
}
