using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class StatUpCanvas : MonoBehaviour
{
    public GameObject[] Options;
    public GameObject text;
    public Material cardShinyMat;
    public TextSetter textSetter;

    private void OnEnable()
    {
        ActiveOptions();
        GameManager.Instance.player.SetExp();
    }

    private void OnDisable()
    {
        DisableAll();
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
