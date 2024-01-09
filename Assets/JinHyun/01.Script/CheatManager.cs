using TMPro;
using UnityEngine;

public class CheatManager : MonoBehaviour
{
    public static CheatManager instance;
    public GameObject text;
    public TMP_Text rankingText;
    private void Awake()
    {
        instance = this;
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.O) && Input.GetKeyDown(KeyCode.P))
        {
            print("����");
            if (GameManager.Instance.player.gameObject.tag == "op")
            {
                text.SetActive(false);
                GameManager.Instance.player.gameObject.tag = "Player";
                GameManager.Instance.playerHealth.gameObject.tag = "Player";
            }
            else
            {
                text.SetActive(true);
                GameManager.Instance.player.gameObject.tag = "op";
                GameManager.Instance.playerHealth.gameObject.tag = "op";
            }
        }
        rankingText.text = "���� ����: " + (Ranking)CheckCard.instance.cheatRankCount;
    }
}
