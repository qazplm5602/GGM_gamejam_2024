using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private void Update() {
        if(Input.GetKeyDown(KeyCode.O)) {
            PoolManager.Instance.Pop("Enemy", Vector3.one);
        }
    }
}
