using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBullet : MonoBehaviour
{
    void Update()
    {
        transform.position += transform.right * Time.deltaTime * 5;
    }
}
