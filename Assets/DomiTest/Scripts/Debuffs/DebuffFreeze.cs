using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffFreeze : MonoBehaviour
{
    EnemyController _controller;
    private void Awake() {
        _controller = GetComponent<EnemyController>();
    }

    private void Start() {
        print("freeze Start");
        StartCoroutine(Freeze());
    }

    IEnumerator Freeze() {
        _controller.moveable = false;
        print("freeze!");
        yield return new WaitForSeconds(2);        
        print("unfreezed.");
        _controller.moveable = true;
        OnDisable();
    }

    private void OnDisable() {
        Destroy(this);
    }
}
