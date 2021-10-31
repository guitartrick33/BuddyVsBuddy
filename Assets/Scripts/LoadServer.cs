using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class LoadServer : MonoBehaviourPunCallbacks
{
    public InputField usernameInput;
    public Text buttonText;
    
    /* Bad words list - used for filtering */
    public BadWordsFilter badWordsFilter;

    public Text errorText;

    private void Start()
    {
        errorText.text = "";
    }

    public void OnClickConnect()
    {
        /* Checks whether nickname value is empty or if it matches any of the bad words, connect to the server if everything is ok */
        if(usernameInput.text.Length >= 1)
        {
            bool isProhibited = false;
            foreach (string word in badWordsFilter.badWords)
            {
                if (usernameInput.text.ToLower() == word.ToLower() || usernameInput.text.ToLower().Contains(word.ToLower()))
                {
                    // usernameInput.text = "";
                    errorText.text = "*Name prohibited";
                    isProhibited = true;
                }
            }
            if (!isProhibited)
            {
                PhotonNetwork.NickName = usernameInput.text;
                buttonText.text = "Connecting...";
                PhotonNetwork.AutomaticallySyncScene = true;
                PhotonNetwork.ConnectUsingSettings();  
            }
            StartCoroutine(WaitErrorText());
        }
        else
        {
            errorText.text = "Enter a name";
            StartCoroutine(WaitErrorText());
        }
    }
    public void OnClickQuit()
    {
        Application.Quit();
    }

    public override void OnConnectedToMaster()
    {
        SceneManager.LoadScene("Lobby");
    }
    
    IEnumerator WaitErrorText()
    {
        yield return new WaitForSeconds(2.5f);
        errorText.text = "";
    }
}
