using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    [Header("Player Info")]
    public Transform playerTrm;
    public PlayerExperience player;
    public Image hpFill;
    public Image expFill;

    public RuntimeAnimatorController[] enemy;
    public GameObject SettingCanvas;

    public int enemyKill = 0;
    public string timer = "00:00";

    float maxHp = 100f;
    public int curHp = 100;

    [SerializeField] private GameObject statCanvas;


    private void Awake()
    {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    private void Start()
    {
        if(hpFill) hpFill.fillAmount = 1;
        if(expFill) expFill.fillAmount = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SettingCanvas.SetActive(!SettingCanvas.activeSelf);
        }
    }
    public void OnStatCanvas()
    {
        statCanvas.SetActive(true);
    }
    public void SetHP(int nowHp)
    {
        hpFill.fillAmount = nowHp / maxHp;
    }
    public void SetEXP(int curExp, int needExp)
    {
        expFill.fillAmount = curExp / (float)needExp;
    }
}
