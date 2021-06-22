using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comics : MonoBehaviour
{
    public GameObject comicsMenuUI;

    void Update()
    {

    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        comicsMenuUI.SetActive(false);
    }
}
