using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject winMenuUI;

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "BossRoom" && Enemy.Instance.bossDead == true)
        {
            winMenuUI.SetActive(true);
        }
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
