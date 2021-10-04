using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private bool Paused = false;

    public AudioSource audio;

    public float musicVolume;

    public RectTransform pausePanel;

    public Slider volumeSlider;

    private void Awake()
    {
        pausePanel.gameObject.SetActive(false);
        Paused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            Paused = !Paused;

            if (Paused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void PauseGame()
    {
        pausePanel.gameObject.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ResumeGame()
    {
        pausePanel.gameObject.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void SetVolume()
    {
        audio.volume = volumeSlider.value;
    }
}
