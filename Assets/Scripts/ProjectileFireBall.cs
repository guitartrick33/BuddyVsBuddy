using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class ProjectileFireBall : MonoBehaviour
{
    private GameObject myPlayer;
    public PhotonView photonView;
    public float baseDamage;
    private GameObject gameManager;
    private bool isDead;
    public void Start()
    {
        gameManager = GameObject.Find("GameManager");
        myPlayer = GameObject.FindGameObjectWithTag("Dummy");
        isDead = false;
    }

    private void Update()
    {
        if (isDead)
        {
            myPlayer.GetComponent<PlayerResources>().AddScore();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (photonView.IsMine)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                if (!collision.gameObject.GetComponent<PlayerRespawner>().hasRespawned)
                {
                    if (myPlayer.GetComponent<Incinerate>().duration)
                    {
                        if(collision.gameObject.GetComponent<PlayerMovement>().classEnum == ClassEnum.WARRIOR && collision.gameObject.GetComponent<Shield>().isShieldActive){}
                        else
                        {
                            collision.gameObject.GetComponent<PlayerResources>().TakeDamage(myPlayer.GetComponent<PhotonView>().ViewID, myPlayer.GetComponent<Fireball>().damage * 2);
                        }
                    }
                    else
                    {
                        if(collision.gameObject.GetComponent<PlayerMovement>().classEnum == ClassEnum.WARRIOR && collision.gameObject.GetComponent<Shield>().isShieldActive){}
                        else
                        {
                            collision.gameObject.GetComponent<PlayerResources>().TakeDamage(myPlayer.GetComponent<PhotonView>().ViewID, myPlayer.GetComponent<Fireball>().damage);
                        }
                    }
                    PhotonNetwork.Instantiate("Impact", transform.position, Quaternion.identity);
                    PhotonNetwork.Destroy(gameObject);
                }
            }

            if (collision.gameObject.tag == "Ground")
            {
                PhotonNetwork.Instantiate("Impact", transform.position, Quaternion.identity);
                PhotonNetwork.Destroy(gameObject);
            }

            if (collision.gameObject.tag == "Item")
            {
                PhotonNetwork.Instantiate("Impact", transform.position, Quaternion.identity);
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }
}


