using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatManager : MonoBehaviour
{
    public static CheatManager instance;
    private void Awake()
    {
        instance = this;
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.O) && Input.GetKeyDown(KeyCode.P))
        {
            print("´­¸²");
            if (GameManager.Instance.player.gameObject.tag == "op")
            {
                GameManager.Instance.player.gameObject.tag = "Player";
                GameManager.Instance.playerHealth.gameObject.tag = "Player";
            }
            else
            {
                GameManager.Instance.player.gameObject.tag = "op";
                GameManager.Instance.playerHealth.gameObject.tag = "op";
            }
        }
    }
}
