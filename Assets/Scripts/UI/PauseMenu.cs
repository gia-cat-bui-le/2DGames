using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject soundPanelUI;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    void Pause()
    {
        audioManager.PlaySFX(audioManager.pause);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Resume()
    {
        audioManager.PlaySFX(audioManager.click);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void SettingGame ()
    {
        audioManager.PlaySFX(audioManager.click);
        Debug.Log("Loading setting...");
        pauseMenuUI.SetActive(false);
        soundPanelUI.SetActive(true);
    }

    public void MenuGame()
    {
        audioManager.PlaySFX(audioManager.click);
        Debug.Log("Loading menu...");
        Time.timeScale = 1f;
        SceneManager.LoadScene("Scenes/Main Menu");
    }

    public void QuitGame()
    {
        audioManager.PlaySFX(audioManager.click);
        Debug.Log("Quitting game!!!");
        Application.Quit();
    }
}
