using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;

public class RespawnManager : MonoBehaviour
{
    public bool isPotionSpawned;
    public bool isCoinSpawned;
    public GameObject[] potionSpawnPoints;
    public GameObject[] coinSpawnPoints;
    public float coinRespawnTimer;
    public float potionRespawnTimer;
    private float potionCounter;
    private float coinCounter;

    private void Start()
    {
        isPotionSpawned = true;
        isCoinSpawned = true;
        
        potionCounter = potionRespawnTimer;
        coinCounter = coinRespawnTimer;
    }

    private void Update()
    {
        if (!isPotionSpawned)
        {
            potionCounter -= Time.deltaTime;
            if (potionCounter <= 0)
            {
                PhotonNetwork.Instantiate("Potion", potionSpawnPoints[Random.Range(0, potionSpawnPoints.Length)].transform.position,
                    Quaternion.identity);
                isPotionSpawned = true;
                potionCounter = potionRespawnTimer;
            }
        }
        
        if (!isCoinSpawned)
        {
            coinCounter -= Time.deltaTime;
            if (coinCounter <= 0)
            {
                PhotonNetwork.Instantiate("Coin", coinSpawnPoints[Random.Range(0, coinSpawnPoints.Length)].transform.position,
                    Quaternion.identity);
                isCoinSpawned = true;
                coinCounter = coinRespawnTimer;
            }
        }
    }
}
