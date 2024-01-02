using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    public Transform playerTrm;

    private void Awake() {
        if(Instance == null) Instance = this;
        else Destroy(gameObject);
    }
}
