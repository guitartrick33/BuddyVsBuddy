using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class MeleeAttack : MonoBehaviour
{
    private Animator animator;
    public Transform attackPoint;
    public float attackRange = 1;
    public LayerMask enemyLayers;
    
    private PhotonView photonView;
    
    public float damage = 15;
    private bool isOnCd;
    public float cooldown = 2;
    private float cooldownTimer;

    private Image meleeAttackCDImage;
    private Text meleeAttackCDText;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        if (photonView.IsMine)
        {
            animator = GetComponent<Animator>();
            cooldownTimer = cooldown;
            isOnCd = false;
            meleeAttackCDText = GameObject.FindWithTag("meleeAttackCDText").GetComponent<Text>();
            meleeAttackCDImage = GameObject.FindWithTag("meleeAttackCDImage").GetComponent<Image>();
        }
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Mouse0) && !gameObject.GetComponent<PlayerRespawner>().hasRespawned)
            {
                if (!isOnCd)
                {
                    animator.SetTrigger("Attack");

                    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
                    foreach (Collider2D enemy in hitEnemies)
                    {
                        if (!enemy.GetComponent<PlayerRespawner>().hasRespawned)
                        {
                            enemy.GetComponent<PlayerResources>().TakeDamage(photonView.ViewID, 15);
                            PhotonNetwork.Instantiate("AxeImpact", enemy.transform.position, Quaternion.identity);
                        }
                    }

                    isOnCd = true;
                }
            }

            if (isOnCd)
            {
                cooldownTimer -= Time.deltaTime;
                meleeAttackCDImage.fillAmount = cooldownTimer / cooldown;
                meleeAttackCDText.text = (cooldownTimer % cooldown).ToString();
                if (meleeAttackCDImage.fillAmount <= 0.05)
                {
                    meleeAttackCDText.text = "";
                }
            }
            else
            {
                meleeAttackCDImage.fillAmount = -1;
            }

            if (cooldownTimer <= 0)
            {
                isOnCd = false;
                cooldownTimer = cooldown;
            }
        }
        
    }
}
