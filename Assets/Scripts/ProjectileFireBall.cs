using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class ProjectileFireBall : MonoBehaviour
{
    private GameObject myPlayer;
    public PhotonView photonView;
    public float baseDamage;
    public void Start()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Dummy");
        foreach (GameObject player in players)
        {
            if (player.GetComponent<PhotonView>().IsMine)
            {
                myPlayer = player;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (photonView.IsMine)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                if (myPlayer.GetComponent<Incinerate>().duration)
                {
                    collision.gameObject.GetComponent<PlayerResources>().TakeDamage(myPlayer.GetComponent<Fireball>().damage * 2);
                    Debug.Log("Hi");
                }
                else
                {
                    collision.gameObject.GetComponent<PlayerResources>().TakeDamage(myPlayer.GetComponent<Fireball>().damage);
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
