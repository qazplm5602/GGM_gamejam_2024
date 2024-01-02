using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private void Update() {
        if(Input.GetKeyDown(KeyCode.O)) {
            PoolManager.Instance.Pop("Boss", Vector3.one);
        }
    }
}
