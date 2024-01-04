using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSetter : MonoBehaviour
{
    private SpriteRenderer sr;

    private void Awake() {
        sr = GetComponent<SpriteRenderer>();
    }
    private void Start() {
        sr.sprite = CharacterManager.instance.GetData().Item1.sprite;
    }
}
