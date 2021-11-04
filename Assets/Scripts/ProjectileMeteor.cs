using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class ProjectileMeteor : MonoBehaviour
{
    public float destructionTimer = 2.5f;
    public float damage = 25;
    private GameObject myPlayer;
    public PhotonView photonView;
    private GameObject gameManager;
    public float blastRadius = 5f;
    private GameObject recastImg;
    public void Start()
    {
        gameManager = GameObject.Find("GameManager");
        recastImg = GameObject.Find("MeteorRecast");
        GameObject[] players = GameObject.FindGameObjectsWithTag("Dummy");
        foreach (GameObject player in players)
        {
            if (player.GetComponent<PhotonView>().IsMine)
            {
                myPlayer = player;
            }
        }

        if (photonView.IsMine)
        {
            recastImg.GetComponent<Image>().enabled = true;
        }
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            destructionTimer -= Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Alpha3) || destructionTimer <= 0)
            {
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                foreach (GameObject enemy in enemies)
                {
                    if (blastRadius >= Vector3.Distance(transform.position, enemy.transform.position))
                    {
                        if (myPlayer.GetComponent<Incinerate>().duration)
                        {
                            enemy.GetComponent<PlayerResources>().TakeDamage(myPlayer.GetComponent<PhotonView>().ViewID, damage * 2);
                        }
                        else
                        {
                            enemy.GetComponent<PlayerResources>().TakeDamage(myPlayer.GetComponent<PhotonView>().ViewID, damage);
                        }
                    }
                }
                recastImg.GetComponent<Image>().enabled = false;
                PhotonNetwork.Instantiate("FireBlast", transform.position, Quaternion.identity);
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (photonView.IsMine)
        {
            // if (collision.gameObject.tag == "Enemy")
            // {
            //     if (myPlayer.GetComponent<Incinerate>().duration)
            //     {
            //         DealDamage(collision.gameObject, damage * 2);
            //     }
            //     else
            //     {
            //         DealDamage(collision.gameObject, damage);
            //     }
            //     PhotonNetwork.Instantiate("Impact", transform.position, Quaternion.identity);
            //     PhotonNetwork.Destroy(gameObject);
            // }

            if (collision.gameObject.tag == "Ground")
            {
                PhotonNetwork.Instantiate("Impact", transform.position, Quaternion.identity);
                PhotonNetwork.Destroy(gameObject);
                recastImg.GetComponent<Image>().enabled = false;
            }

            if (collision.gameObject.tag == "Item")
            {
                PhotonNetwork.Instantiate("Impact", transform.position, Quaternion.identity);
                PhotonNetwork.Destroy(gameObject);
                recastImg.GetComponent<Image>().enabled = false;
            }
        }
    }
}
