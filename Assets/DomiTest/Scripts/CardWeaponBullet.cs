using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardWeaponBullet : MonoBehaviour
{
    public int damage = 1;
    public float speed = 5;
    
    void Update()
    {
        transform.position += transform.right * Time.deltaTime * speed;
    }
}
