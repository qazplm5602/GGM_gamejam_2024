using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffFire : MonoBehaviour
{
    public int damage = 0;
    public GameObject effectEntity;

    EnemyController controller;

    private void Awake() {
        controller = GetComponent<EnemyController>();
    }

    private void Start() {
        print("fire Start Tick");
        StartCoroutine(Tick());
    }
    
    IEnumerator Tick() {
        for (int i = 0; i < 4; i++)
        {
            controller.Hit(damage);
            print($"fire debuff damage {damage} / "+i+" / "+gameObject.name);
            yield return new WaitForSeconds(1);
        }
        
        print("end fire");
        // Destroy(this);
        OnDisable();
    }

    private void OnDisable() {
        Destroy(this);
    }
}
