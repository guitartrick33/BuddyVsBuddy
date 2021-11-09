using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class AxeProjectile : MonoBehaviour
{
    public float damage = 20;
    private GameObject myPlayer;
    public PhotonView photonView;
    private PlayerResources pr;
    private GameObject gameManager;
    public float slowDuration;
    private float slowDurationTimer;
    public void Start()
    {
        gameManager = GameObject.Find("GameManager");
        GameObject[] players = GameObject.FindGameObjectsWithTag("Dummy");
        foreach (GameObject player in players)
        {
            if (player.GetComponent<PhotonView>().IsMine)
            {
                myPlayer = player;
            }
        }

        slowDurationTimer = slowDuration;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (photonView.IsMine)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                if (!collision.gameObject.GetComponent<PlayerRespawner>().hasRespawned)
                {
                    if(collision.gameObject.GetComponent<PlayerMovement>().classEnum == ClassEnum.WARRIOR && collision.gameObject.GetComponent<Shield>().isShieldActive){}
                    else
                    {
                        collision.gameObject.GetComponent<PlayerResources>().TakeDamage(myPlayer.GetComponent<PhotonView>().ViewID, damage);
                        photonView.RPC("SlowDown", RpcTarget.All, collision.gameObject.GetComponent<PhotonView>().ViewID);
                    }
                    PhotonNetwork.Instantiate("AxeImpact", transform.position, Quaternion.identity);
                    PhotonNetwork.Destroy(gameObject);
                }
            }

            if (collision.gameObject.tag == "Ground")
            {
                PhotonNetwork.Destroy(gameObject);
            }

            if (collision.gameObject.tag == "Item")
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }

    [PunRPC]
    void SlowDown(int id)
    {
        PhotonView.Find(id).gameObject.GetComponent<PlayerMovement>().speed *= 0.2f;
        PhotonView.Find(id).gameObject.GetComponent<PlayerMovement>().isSlowed = true;
    }
}
