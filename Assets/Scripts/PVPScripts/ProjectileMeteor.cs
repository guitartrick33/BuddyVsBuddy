using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class ProjectileMeteor : MonoBehaviour
{
    public GameObject impactEffect;
    private Rigidbody2D rb;
    public float damage = 20;
    private PlayerResources playerResources;
    public float blastRadius = 10f;
    private GameObject myPlayer;
    private GameObject enemyPlayer;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // playerResources = GameObject.FindWithTag("Player").GetComponent<PlayerResources>();
        GameObject[] players = GameObject.FindGameObjectsWithTag("Dummy");
        foreach (GameObject player in players)
        {
            if (PhotonView.Get(player).IsMine)
            {
                myPlayer = player;
            }
            else if (!PhotonView.Get(player).IsMine)
            {
                enemyPlayer = player;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == enemyPlayer)
        {
            Debug.Log("Hit");
        } 
    }
}
