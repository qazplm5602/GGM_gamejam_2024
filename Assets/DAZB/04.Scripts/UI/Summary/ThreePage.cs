using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ThreePage : MonoBehaviour
{
    public TMP_Text[] texts;
    private TextSetter textSetter;

    private void Awake() {
        textSetter = GetComponentInChildren<TextSetter>();
    }

    private void OnEnable() {
        textSetter.SetPercentText();
    }
}
