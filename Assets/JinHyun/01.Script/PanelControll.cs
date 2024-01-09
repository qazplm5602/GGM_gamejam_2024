using UnityEngine;

public class PanelControll : MonoBehaviour
{
    public GameObject[] panels;
    public GameObject clickShield;

    private void OnDisable()
    {
        foreach (GameObject item in panels)
        {
            item.SetActive(false);
        }
        clickShield.SetActive(false);
        Time.timeScale = 1f;
    }

    private void OnEnable()
    {
        clickShield.SetActive(true);
        Time.timeScale = 0;
    }
}
