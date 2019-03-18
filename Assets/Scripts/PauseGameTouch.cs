using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGameTouch : MonoBehaviour
{
    public GameObject pausePanel;

    bool pauseState;

    void Update()
    {
        if (pauseState)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0.0f;
        }
        else
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1.0f;
        }
    }

    public void flipState()
    {
        pauseState = !pauseState;
    }
}
