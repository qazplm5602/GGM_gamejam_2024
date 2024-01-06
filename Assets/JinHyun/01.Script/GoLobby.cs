using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoLobby : MonoBehaviour
{
    public void GotoMain() {
        Destroy(GameManager.Instance);
        CheckCard.instance.ResetCounter();
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
