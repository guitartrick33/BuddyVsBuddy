using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Photon.Pun;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Projectile : MonoBehaviour
{
    public float damage = 20;
    private GameObject myPlayer;
    public PhotonView photonView;
    private PlayerResources pr;
    public float baseDamage;
    private GameObject gameManager;
    public void Start()
    {
        gameManager = GameObject.Find("GameManager");
        // playerResources = GameObject.FindWithTag("Player").GetComponent<PlayerResources>();
        GameObject[] players = GameObject.FindGameObjectsWithTag("Dummy");
        foreach (GameObject player in players)
        {
            if (player.GetComponent<PhotonView>().IsMine)
            {
                myPlayer = player;
            }
        }

        damage = baseDamage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (photonView.IsMine)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                if (myPlayer.GetComponent<Incinerate>().duration)
                {
                    collision.gameObject.GetComponent<PlayerResources>().TakeDamage(myPlayer.GetComponent<PhotonView>().ViewID, damage * 2);
                }
                else
                {
                    collision.gameObject.GetComponent<PlayerResources>().TakeDamage(myPlayer.GetComponent<PhotonView>().ViewID, damage);
                }
                PhotonNetwork.Instantiate("Impact", transform.position, Quaternion.identity);
                PhotonNetwork.Destroy(gameObject);
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
