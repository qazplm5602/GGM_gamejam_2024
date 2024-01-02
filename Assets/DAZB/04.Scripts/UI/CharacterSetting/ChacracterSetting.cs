using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ChacracterSetting : MonoBehaviour
{  
    public WeaponSelection[] selections;

    private void Awake() {
        selections = GetComponentsInChildren<WeaponSelection>();    
    }
    
    public void HoverMouse(GameObject obj) {
        obj.GetComponent<RectTransform>().DOScale(new Vector3(0.85f, 0.85f, 0.85f), 0.15f);
    }

    public void UnHoverMouse(GameObject obj) {
        obj.GetComponent<RectTransform>().DOScale(new Vector3(0.7f + 0.3f, 0.7f + 0.3f, 0.7f + 0.3f), 0.15f);
    }

    public void AllSelectFalse() {
        foreach (WeaponSelection iter in selections) {
            iter.SelectFales();
        }
    }
}
