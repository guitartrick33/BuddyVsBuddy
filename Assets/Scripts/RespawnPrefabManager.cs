using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class RespawnPrefabManager : MonoBehaviourPun
{
    [SerializeField] private PhotonView photonView;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Dummy")
        {
            photonView.RPC("SetInactiveObjectRPC", RpcTarget.All, photonView.ViewID);
        }  
    }

    [PunRPC]
    void SetInactiveObjectRPC(int id)
    {
        PhotonView.Find(id).gameObject.SetActive(false);
    }
}