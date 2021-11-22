using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerRespawner : MonoBehaviour
{
    private PhotonView photonView;
    public float respawnTimer;
    public bool hasRespawned;
    [SerializeField] private Color respawnColor;
    [SerializeField] private Color normalColor;

    private void Start()
    {
        photonView = gameObject.GetComponent<PhotonView>();
    }

    public void PlayerRespawnTimer()
    {
        photonView.RPC("SetPlayerRespawnModeRPC", RpcTarget.All, null);
        StartCoroutine(WaitForRespawnTimer());
    }

    public void StartRespawnTimer()
    {
        photonView.RPC("SetPlayerRespawnModeRPC", RpcTarget.All, null);
        StartCoroutine(WaitForRespawnTimerStart());
    }

    IEnumerator WaitForRespawnTimer()
    {
        yield return new WaitForSeconds(respawnTimer);
        photonView.RPC("SetPlayerNormalModeRPC", RpcTarget.All, null);
    }
    
    IEnumerator WaitForRespawnTimerStart()
    {
        yield return new WaitForSeconds(6);
        photonView.RPC("SetPlayerNormalModeRPC", RpcTarget.All, null);
    }

    [PunRPC]
    void SetPlayerRespawnModeRPC()
    {
        gameObject.GetComponent<SpriteRenderer>().color = respawnColor;
        hasRespawned = true;
    }

    [PunRPC]
    void SetPlayerNormalModeRPC()
    {
        gameObject.GetComponent<SpriteRenderer>().color = normalColor;
        hasRespawned = false;
    }
}
