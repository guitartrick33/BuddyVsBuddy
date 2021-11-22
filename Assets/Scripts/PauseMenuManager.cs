using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pausePanel;
    private bool isActive;
    private GameObject player;

    private void Start()
    {
        pausePanel.SetActive(false);
        isActive = false;
    }

    public void OpenPauseMenu()
    {
        if (isActive)
        {
            pausePanel.SetActive(false);
            isActive = false;
        }
        else if (isActive == false)
        {
            pausePanel.SetActive(true);
            isActive = true;
        }
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
        isActive = false;
    }

    public void BackToLobby()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isActive)
            {
                pausePanel.SetActive(false);
                isActive = false;
            }
            else if (isActive == false)
            {
                pausePanel.SetActive(true);
                isActive = true;
            }
        }
       
    }
}
