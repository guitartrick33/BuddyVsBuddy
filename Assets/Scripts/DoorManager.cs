using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class DoorManager : MonoBehaviourPunCallbacks
{
    public Sprite closedDoor;
    public Sprite openDoor;

    private SpriteRenderer spriteRenderer;

    public bool isOpen;

    private BoxCollider2D collider2D;

    private PhotonView photonView;

    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        isOpen = false;
        collider2D = gameObject.GetComponent<BoxCollider2D>();
        photonView = gameObject.GetComponent<PhotonView>();
    }

    private void Update()
    {
            if (isOpen)
            {
                photonView.RPC("OpenDoor", RpcTarget.All, null);
            }

            if (!isOpen)
            {
                photonView.RPC("CloseDoor", RpcTarget.All, null);
            }
    }

    [PunRPC]
    private void OpenDoor()
    {
        spriteRenderer.sprite = openDoor;
        collider2D.enabled = false;
    }
    
    [PunRPC]
    private void CloseDoor()
    {
        spriteRenderer.sprite = closedDoor;
        collider2D.enabled = true;
    }
    
}
