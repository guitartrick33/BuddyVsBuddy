using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class LeverManager : MonoBehaviourPunCallbacks
{
    public Sprite leftLever;
    public Sprite rightLever;

    private SpriteRenderer spriteRenderer;

    public Text leverText;

    public bool isTurned;
    public GameObject door;

    private bool isTrigger;

    private PhotonView photonView;

    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        leverText.text = "";
        isTurned = false;
        isTrigger = false;
        photonView = gameObject.GetComponent<PhotonView>();
        door.GetComponent<DoorManager>().isOpen = false;
    }

    private void Update()
    {
        
        if (isTurned)
        {
            photonView.RPC("TurnRightLever", RpcTarget.All, null);
            door.GetComponent<DoorManager>().isOpen = true;
        }

        if (!isTurned)
        {
            photonView.RPC("TurnLeftLever", RpcTarget.All, null);
            door.GetComponent<DoorManager>().isOpen = false;
        }

        if (isTrigger && Input.GetKeyUp(KeyCode.E))
        {
            Debug.Log("Lol");
            photonView.RPC("Turn", RpcTarget.All, null);
        }

        if (photonView.IsMine)
        {
            LeverTextCallback();
        }
    }
    
    [PunRPC]
    public void Turn()
    {
        if (isTurned == false)
        {
            isTurned = true;
        }
        else if (isTurned)
        {
            isTurned = false;
        }
    }

    [PunRPC] private void TurnRightLever()
    {
        spriteRenderer.sprite = rightLever;
    }
    
    [PunRPC] private void TurnLeftLever()
    {
        spriteRenderer.sprite = leftLever;
    }

    private void LeverTextCallback()
    {
        if (isTrigger)
        {
            leverText.text = "Press 'E' to turn";
        }
        else if (!isTrigger)
        {
            leverText.text = "";
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isTrigger = false;
        }
    }
}
