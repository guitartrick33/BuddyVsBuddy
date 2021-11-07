using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    private Rigidbody2D rb;

    public float dashDistance = 15f;
    private float doubleTapTime;
    private KeyCode lastKeyCode;
    private float gravity;
    private float facingDir;
    public bool isOnCooldown;
    public float cooldownTimer;
    private float cooldownCounter;
    public bool isDashing;
    private Vector2 lookDirection;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cooldownCounter = cooldownTimer;
        gravity = rb.gravityScale;
        facingDir = 1;
        isDashing = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            // if (doubleTapTime > Time.deltaTime && lastKeyCode == KeyCode.A && !isOnCooldown)
            // {
            //     StartCoroutine(DashDir(-1f));
            //     isOnCooldown = true;
            // }
            // else
            // {
            //     doubleTapTime = Time.time + 0.1f;
            // }

            lastKeyCode = KeyCode.A;
        }
        
        if (Input.GetKeyDown(KeyCode.D))
        {
            // if (doubleTapTime > Time.deltaTime && lastKeyCode == KeyCode.D && !isOnCooldown)
            // {
            //     StartCoroutine(DashDir(1f));
            //     isOnCooldown = true;
            // }
            // else
            // {
            //     doubleTapTime = Time.time + 0.1f;
            // }

            lastKeyCode = KeyCode.D;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // if (lastKeyCode == KeyCode.A && !isOnCooldown)
            // {
            //     StartCoroutine(DashDir(-1f));
            //     isOnCooldown = true;
            // }
            // else if (lastKeyCode == KeyCode.D && !isOnCooldown)
            // {
            //     StartCoroutine(DashDir(1f));
            //     isOnCooldown = false;
            // }
            if (!isOnCooldown)
            {
                StartCoroutine(DashDir());
                isOnCooldown = true;
            }
            
        }

        if (isOnCooldown)
        {
            cooldownCounter -= Time.deltaTime;
            if (cooldownCounter <= 0)
            {
                isOnCooldown = false;
                cooldownCounter = cooldownTimer;
            }
        }
    }

    IEnumerator DashDir()
    {
        lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - new Vector3(gameObject.transform.position.x, gameObject.transform.position.y);
        isDashing = true;
        // rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(lookDirection, ForceMode2D.Impulse);
        // rb.gravityScale = 0;
        yield return new WaitForSeconds(0.4f);
        rb.gravityScale = gravity;
        isDashing = false;
    }
}
