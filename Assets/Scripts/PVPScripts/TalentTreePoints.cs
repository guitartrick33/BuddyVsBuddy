using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class TalentTreePoints : MonoBehaviour
{
    private PlayerResources pr;
    private GameObject myPlayer;
    public Text talentPoints;
    void Start()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Dummy");
        foreach (GameObject player in players)
        {
            if (player.GetComponent<PhotonView>().IsMine)
            {
                myPlayer = player;
            }
        }

        pr = myPlayer.GetComponent<PlayerResources>();
    }

    private void Update()
    {
        talentPoints.text = "Talent points: " + pr.currentTalentPoints;
    }
}
