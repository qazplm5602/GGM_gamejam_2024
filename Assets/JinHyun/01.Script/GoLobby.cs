using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoLobby : MonoBehaviour
{
    public void GotoMain() {
        Destroy(GameManager.Instance);
         UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
