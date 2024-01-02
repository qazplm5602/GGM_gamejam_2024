using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    public Transform playerTrm;

    [SerializeField] private GameObject statCanvas;

    public PlayerExperience player;

    private void Awake() {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    
    public void OnStatCanvas()
    {
        statCanvas.SetActive(true);
    }
}
