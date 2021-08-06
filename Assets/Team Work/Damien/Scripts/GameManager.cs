using System;
using System.Collections;
using System.Collections.Generic;
using Damien;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool paused;

    private Menu menu;

    public GameObject pauseMenu;

    public GameObject optionsMenu;
    // Start is called before the first frame update
    void Awake()
    {
        bool paused = false;
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (!paused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0;
        paused = true;
        pauseMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    void ResumeGame()
    {
        Time.timeScale = 1;
        paused = false;
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(false);
    }
}
