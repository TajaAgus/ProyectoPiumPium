using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;

    private bool isGamePaused = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            isGamePaused = !isGamePaused; // Alterna una booleana.
            PauseGame();
        }
    }

    private void PauseGame()
    {
        if (isGamePaused)
        {
            Time.timeScale = 0;

            pausePanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;

            pausePanel.SetActive(false);
        }
    }
}
