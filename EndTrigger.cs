using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTrigger : MonoBehaviour
{
    public gameManager gameManager;

    void OnTriggerEnter()
    {
        gameManager.CompleteLevel();
        Time.timeScale = 1f;
    }
}
