using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponSelection : MonoBehaviour
{
    public Button[] children;
    public WeaponData[] weaponDatas;
    public WeaponType nowWeaponType;
    public Image nowWeaponImage;

    private void Awake() {
        children = GetComponentsInChildren<Button>();
    }

    public void SelectTrue(int idx) {
        SelectAllFalse();
        children[idx].GetComponent<Outline>().effectDistance = new Vector2(7, -7);
        children[idx].GetComponentInChildren<TMP_Text>().color = Color.yellow;
        SetWeapon(idx);
    }

    public void SelectFalse(int idx) {
        children[idx].GetComponent<Outline>().effectDistance = new Vector2(1, -1);
        children[idx].GetComponentInChildren<TMP_Text>().color = Color.white;
    }

    public void SelectAllFalse() {
        for (int i = 0; i < children.Length; ++i) {
            SelectFalse(i);
        }
    }

    public void SetWeapon(int idx) {
        nowWeaponImage.sprite = weaponDatas[idx].weaponSprite;
        nowWeaponType = weaponDatas[idx].WeaponType;
    }
}