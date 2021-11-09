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

    public float healthPercentage;

    public HealthBar healthBar;

    private PlayerMovement pm;

    public int killsToWin = 5;

    public bool isWinner;
    private GameObject gameManager;

    public float coinRespawnTime = 5f;
    public float potionHealAmount = 50f;

    public GameObject lastPersonToHitMe;

    [SerializeField] private GameObject onCoinCollectImage;

    private PlayerRespawner respawner;

    void Start()
    {
        respawner = gameObject.GetComponent<PlayerRespawner>();
        onCoinCollectImage.SetActive(false);
        gameManager = GameObject.Find("GameManager");
        currentHealth = maxHealth;
        currentTalentPoints = 16;
        photonView = gameObject.GetComponent<PhotonView>();
        pm = gameObject.GetComponent<PlayerMovement>();
        isWinner = false;
        winnerNickname = GameObject.FindWithTag("winnerName");
        healthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            if (lastPersonToHitMe != null)
            {
                photonView.RPC("ResetPosRPC", RpcTarget.All, null);
                respawner.PlayerRespawnTimer();
                lastPersonToHitMe.GetComponent<PlayerResources>().SubtractScore();
                photonView.RPC("ResetLatestPersonRPC", RpcTarget.All, null);
            }
            else
            {
                photonView.RPC("ResetPosRPC", RpcTarget.All, null);
                respawner.PlayerRespawnTimer();
            }
        }
    }

    public void TakeDamage(int id, float amount)
    {
        photonView.RPC("TakeDamageRPC", RpcTarget.All, amount, id);
    }

    IEnumerator WaitForSeconds()
    {
        yield return new WaitForSeconds(2);
        onCoinCollectImage.SetActive(false);
    }

    public void AddScore()
    {
        photonView.RPC("AddScoreRPC", RpcTarget.All, null);
    }

    public void SubtractScore()
    {
        photonView.RPC("SubtractScoreRPC", RpcTarget.All, null);
    }

    void Heal(float amount)
    {
        photonView.RPC("HealRPC", RpcTarget.All, amount);
    }

    public void IncreaseHealth(float amount)
    {
        photonView.RPC("IncreaseHealthRPC", RpcTarget.All, amount);
    }

    [PunRPC]
    void ResetLatestPersonRPC()
    {
        lastPersonToHitMe = null;
    }
    
    [PunRPC]
    void TakeDamageRPC(float amount, int id)
    {
        {
            currentHealth -= amount;
            healthBar.SetHealthRPC(currentHealth);
            lastPersonToHitMe = PhotonView.Find(id).gameObject;  
        }
    }


    [PunRPC]
    public void ResetPosRPC()
    {
        currentHealth = maxHealth;
        gameObject.transform.position = pm.startPos;
        currentTalentPoints++;
        healthBar.SetHealthRPC(currentHealth);
    }

    [PunRPC]
    void LoadLastScene()
    {
        winnerNickname.GetComponent<SaveNickName>().winnerNick = pm.nickName.text;
        SceneManager.LoadScene("gameover");
    }

    [PunRPC]
    void AddScoreRPC()
    {
        killsToWin++;
    }

    [PunRPC]
    void SubtractScoreRPC()
    {
        killsToWin--;
        if (killsToWin <= 0)
        {
            killsToWin = 0;
        }
    }

    [PunRPC]
    void SetActiveObjectRPC(int id)
    {
        PhotonView.Find(id).gameObject.SetActive(true);
    }

    [PunRPC]
    void IncreaseHealthRPC(float amount)
    {
        maxHealth += amount;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    [PunRPC]
    void HealRPC(float amount)
    {
        currentHealth += amount;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (photonView.IsMine)
        {
            if (other.gameObject.tag == "Crown" && killsToWin <= 0)
            {
                isWinner = true;
                photonView.RPC("LoadLastScene", RpcTarget.All, null);
            }

            if (other.gameObject.tag == "Coin")
            {
                onCoinCollectImage.SetActive(true);
                currentTalentPoints++;
                StartCoroutine(WaitForSeconds());
                gameManager.GetComponent<RespawnManager>().isCoinSpawned = false;
            }

            if (other.gameObject.tag == "Potion")
            {
                healthPercentage = Mathf.RoundToInt(currentHealth / maxHealth);
                Heal(potionHealAmount);
                healthBar.SetHealthRPC(currentHealth);
                gameManager.GetComponent<RespawnManager>().isPotionSpawned = false;
            }
        }
    }
}
