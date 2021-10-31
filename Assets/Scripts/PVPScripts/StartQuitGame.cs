using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartQuitGame : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("ConnectToServer");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Application quit successful");
    }
}
