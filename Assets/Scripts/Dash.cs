using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class Dash : MonoBehaviour
{
    private Rigidbody2D rb;

    public float dashDistance = 15f;
    private float doubleTapTime;
    private KeyCode lastKeyCode;
    private float gravity;
    private float facingDir;
    public bool isOnCooldown;
    public float cooldown;
    public float cooldownTimer;
    public bool isDashing;
    private Vector2 lookDirection;
    
    private Image dashCDImage;
    private Text dashCDText;
    private PhotonView photonView;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        if (photonView.IsMine)
        {
            rb = GetComponent<Rigidbody2D>();
            cooldownTimer = cooldown;
            gravity = rb.gravityScale;
            facingDir = 1;
            isDashing = false;
            dashCDImage = GameObject.FindWithTag("dashImage").GetComponent<Image>();
            dashCDText = GameObject.FindWithTag("dashText").GetComponent<Text>(); 
        }
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                lastKeyCode = KeyCode.A;
            }
        
            if (Input.GetKeyDown(KeyCode.D))
            {
                lastKeyCode = KeyCode.D;
            }

            if (Input.GetKeyDown(KeyCode.Alpha4) && !gameObject.GetComponent<PlayerRespawner>().hasRespawned)
            {
                if (!isOnCooldown)
                {
                    if (lastKeyCode == KeyCode.A)
                    {
                        StartCoroutine(DashDir(-1f));
                        isOnCooldown = true;
                    }
                    else if (lastKeyCode == KeyCode.D)
                    {
                        StartCoroutine(DashDir(1f));
                        isOnCooldown = true;
                    }
                    
                }
            }

            if (isOnCooldown)
            {
                cooldownTimer -= Time.deltaTime;
                dashCDImage.fillAmount = cooldownTimer / cooldown;
                dashCDText.text = (cooldownTimer % cooldown).ToString();
                if (dashCDImage.fillAmount <= 0.05)
                {
                    dashCDText.text = "";
                }
            }
            else
            {
                dashCDImage.fillAmount = -1;
            }
        
            if (cooldownTimer<= 0)
            {
                isOnCooldown = false;
                cooldownTimer = cooldown;
            }
        }
    }

    IEnumerator DashDir(float direction)
    {
        isDashing = true;
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(new Vector2(dashDistance * direction, 0f), ForceMode2D.Impulse);
        rb.gravityScale = 0;
        yield return new WaitForSeconds(0.4f);
        rb.gravityScale = gravity;
        isDashing = false;
    }
}
