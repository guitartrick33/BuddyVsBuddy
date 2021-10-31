using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinHandler : MonoBehaviour
{
    public GameObject button;
    private GameObject winnerNickname;
    public Text winnerText;

    private void Start()
    {
        
    }

    private void Update()
    {
        winnerNickname = GameObject.FindWithTag("winnerName");
        winnerText.text = $"Congratulations {winnerNickname.GetComponent<SaveNickName>().winnerNick} for being a nerd!";
        if (PhotonNetwork.IsMasterClient)
        {
            button.SetActive(true);
        }
        else
        {
            button.SetActive(false);
        }
    }

    public void ReloadScene()
    {
        PhotonNetwork.LoadLevel("testscene");
    }
}
