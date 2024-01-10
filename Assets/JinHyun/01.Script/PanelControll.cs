using UnityEngine;

public class PanelControll : MonoBehaviour
{
    public GameObject[] panels;
    public GameObject clickShield;
    public WeaponBullet playerBulletSys;

    private void OnDisable()
    {
        foreach (GameObject item in panels)
        {
            item.SetActive(false);
        }
        clickShield.SetActive(false);
        playerBulletSys.fireDisable = false;
        Time.timeScale = 1f;
    }

    private void OnEnable()
    {
        clickShield.SetActive(true);
        playerBulletSys.fireDisable = true;
        Time.timeScale = 0;
    }
}
