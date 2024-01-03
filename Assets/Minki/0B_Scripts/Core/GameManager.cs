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

    public GameObject SettingCanvas;

    float maxHp = 100;
    int curHp = 100;

    [SerializeField] private GameObject statCanvas;


    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        //플레이어 받아올거임 근데 뭐 다른방법뭐하지 흠
        //float maxHp = player.GetComponent<>
    }

    private void Start()
    {
        hpFill.fillAmount = 1;
        expFill.fillAmount = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            curHp -= 7;
            SetHP(curHp);
        }

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
        print("아");
        expFill.fillAmount = curExp / (float)needExp;
    }
}
