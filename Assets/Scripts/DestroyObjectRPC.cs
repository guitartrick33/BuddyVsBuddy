using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DestroyObjectRPC : MonoBehaviourPunCallbacks
{
    private PhotonView photonView;


    private void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Dummy")
        {
            photonView.RPC("DestroyObject", RpcTarget.All, null);
        }
    }

    [PunRPC]
    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
