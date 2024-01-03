using System.Threading.Tasks;
using UnityEngine;

public class StatUpCanvas : MonoBehaviour
{
    public GameObject[] Options;
    public GameObject text;
    public Material cardShinyMat;
    public TextSetter textSetter;

    bool firstLevelUp = true;

    private void OnEnable()
    {
        ActiveOptions();
        if (firstLevelUp)
        {
            firstLevelUp = false;
            GameManager.Instance.player.ChangeLevel();
        }
        GameManager.Instance.player.SetExp();
    }

    private void OnDisable()
    {
        DisableAll();
    }

    private void Update()
    {
        Time.timeScale = 0;
    }

    async void ActiveOptions()
    {
        foreach (var item in Options)
        {
            item.SetActive(true);
            await Task.Delay(50);
        }
    }
    public void DisableAll()
    {
        foreach (var item in Options)
        {
            item.SetActive(false);
        }
        text.SetActive(false);
        textSetter.SetPercentText();
        gameObject.SetActive(false);
    }

    public void DisableText()
    {
        text.SetActive(false);
    }
}
