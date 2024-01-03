using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] bosses;

    private int bossCounter = 0;
    private float seconds = 0;

    private void Update() {
        seconds += Time.deltaTime;

        if(seconds > 60f) {
            seconds -= 60f;
            Instantiate(bosses[bossCounter++], transform.position, Quaternion.identity);
        }
    }
}
