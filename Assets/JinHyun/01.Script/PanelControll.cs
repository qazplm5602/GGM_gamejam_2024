using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelControll : MonoBehaviour
{
    public GameObject[] panels;

    private void OnDisable()
    {
        foreach (GameObject item in panels)
        {
            item.SetActive(false);
        }
        Time.timeScale = 1f;
    }

    private void OnEnable()
    {
        Time.timeScale = 0;
    }
}
