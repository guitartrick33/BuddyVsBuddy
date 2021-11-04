using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ScoringSystemManager : MonoBehaviour
{
    private GameObject myPlayer;
    private int counter;
    public Transform parent;
    public GameObject scoreObject;
    private PhotonView photonView;

    private GameObject myPlayerName;

    private bool isCrownInstantiated = false;
    public GameObject spawnPoint;
    private void Start()
    {
        counter = 0;
        myPlayer = GameObject.FindGameObjectWithTag("Dummy");
        photonView = gameObject.GetComponent<PhotonView>();
    }

    private void Update()
    {
        RefreshScore();
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (photonView.IsMine)
        {
            foreach (GameObject g in enemies)
            {
                if ((g.GetComponent<PlayerResources>().killsToWin == 0 && !isCrownInstantiated) ||
                    (myPlayer.GetComponent<PlayerResources>().killsToWin == 0 && !isCrownInstantiated))
                {
                    PhotonNetwork.Instantiate("Crown", spawnPoint.transform.position, Quaternion.identity);
                    isCrownInstantiated = true;
                }
            } 
        }
    }

    public void RefreshScore()
    {
        foreach (Transform child in parent)
        {
            Destroy(child.gameObject);
        }
        counter = 0;
        myPlayerName = Instantiate(scoreObject, transform.position, Quaternion.identity, parent.transform);
        myPlayerName.GetComponent<ScorePrefabManager>().SetPlayerText(myPlayer.GetComponent<PlayerMovement>().nickName.text);
        myPlayerName.GetComponent<ScorePrefabManager>().SetScoreText("Kills to win: " + myPlayer.GetComponent<PlayerResources>().killsToWin.ToString());
        GameObject[] players = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject player in players)
        {
            if (counter < players.Length)
            {
                GameObject prefabClone = Instantiate(scoreObject, transform.position, Quaternion.identity, parent.transform);
                prefabClone.GetComponent<ScorePrefabManager>().SetPlayerText(player.GetComponent<PlayerMovement>().nickName.text);
                prefabClone.GetComponent<ScorePrefabManager>().SetScoreText("Kills to win: " + player.GetComponent<PlayerResources>().killsToWin.ToString());
                counter++;
            }
        }
    }
}
