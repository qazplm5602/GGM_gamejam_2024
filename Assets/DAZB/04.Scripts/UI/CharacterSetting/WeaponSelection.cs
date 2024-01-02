using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponSelection : MonoBehaviour
{
    public bool isSelected;
    public ChacracterSetting chacracterSetting;
    public TMP_Text text;

    private void Awake() {
        chacracterSetting = FindObjectOfType<ChacracterSetting>();
        text = GetComponentInChildren<TMP_Text>();
    }

    public void SelectTrue() {
        isSelected = true;
        chacracterSetting.AllSelectFalse();
        GetComponent<Outline>().effectDistance = new Vector2(7, -7);
        text.color = Color.yellow;
    }

    public void SelectFales() {
        isSelected = false;
        GetComponent<Outline>().effectDistance = new Vector2(1, -1);
        text.color = Color.white;
    }
}
