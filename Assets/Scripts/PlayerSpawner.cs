using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerSpawner : MonoBehaviourPun
{
    public GameObject[] playerPrefabs;
    public Transform[] spawnPoints;
    private Vector3 spawnPos;
    private Quaternion spawnRot;
    private void Start()
    {
        if (PhotonNetwork.LocalPlayer.ActorNumber % 2 == 0)
        {
            spawnPos = spawnPoints[0].transform.position;
            spawnRot = spawnPoints[0].rotation;
        }
        else
        {
            spawnPos = spawnPoints[1].transform.position;
            spawnRot = spawnPoints[1].rotation;
        }
        
        GameObject playerToSpawn = playerPrefabs[(int) PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"]];
        PhotonNetwork.Instantiate(playerToSpawn.name, spawnPos, spawnRot);

        PhotonNetwork.CurrentRoom.IsVisible = false;
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.AutomaticallySyncScene = true;
        
    }

    private void Update()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Dummy");
        foreach (GameObject player in players)
        {
            if (!PhotonView.Get(player).IsMine)
            {
                player.tag = "Enemy";
            }
        }
    }
}
