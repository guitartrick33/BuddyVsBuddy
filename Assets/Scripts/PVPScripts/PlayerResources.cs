using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerResources : MonoBehaviour
{

    public float currentHealth;
    public float maxHealth;
    private GameObject winnerNickname;


    public int currentTalentPoints;


    private PhotonView photonView;
    
    private float healthPercentage;
    
    public HealthBar healthBar;

    private PlayerMovement pm;

    public bool isWinner;
    void Start()
    {
        currentHealth = maxHealth;
        currentTalentPoints = 16;
        photonView = gameObject.GetComponent<PhotonView>();
        pm = gameObject.GetComponent<PlayerMovement>();
        isWinner = false;
        winnerNickname = GameObject.FindWithTag("winnerName");
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        healthPercentage = Mathf.RoundToInt(currentHealth / maxHealth * 100);
        

    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        healthPercentage = Mathf.RoundToInt(currentHealth / maxHealth * 100);
        if (currentHealth <= 0)
        {
            currentHealth = maxHealth;
            gameObject.transform.position = pm.startPos;
        }
        healthBar.SetHealthRPC(currentHealth);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (photonView.IsMine)
        {
            if (other.gameObject.tag == "Crown")
            {
                isWinner = true;
                photonView.RPC("LoadLastScene", RpcTarget.All, null);
            }
        }
    }

    [PunRPC]
    void LoadLastScene()
    {
        winnerNickname.GetComponent<SaveNickName>().winnerNick = pm.nickName.text;
        SceneManager.LoadScene("gameover");
    }
    
    
}
