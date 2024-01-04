using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] bosses;

    private int bossCounter = 4;
    private float seconds = 110;

    private void Update() {
        seconds += Time.deltaTime;

        if(seconds > 120f) {
            seconds -= 120f;
            Instantiate(bosses[bossCounter++], transform.position, Quaternion.identity);
        }
    }
}
