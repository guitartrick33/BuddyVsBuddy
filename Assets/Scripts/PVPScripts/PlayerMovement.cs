﻿using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon.StructWrapping;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public Vector3 startPos;
    public PhotonView photonView;

    public float speed;

    public float jumpForce;
    private float moveInput;
    private float ropeInput;

    private Rigidbody2D rb;
    private FallDamage fd;

    private bool isOnRope;
    public bool isGrounded;
    public bool isWater;
    public bool isLava;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    public LayerMask whatIsWater;

    private int extraJumps;
    public int extraJumpValue;

    public bool facingRight = true;

    public Text nickName;
    public SpriteRenderer sprite;

    [SerializeField] private Dash dash;
    public ClassEnum classEnum;
    private Animator animator;
    public bool isSlowed;
    private float slowTimer;
    private float startSpeed;
    
    
    void Start()
    {
        if (classEnum == ClassEnum.WARRIOR)
        {
            dash = GetComponent<Dash>();
            animator = GetComponent<Animator>();
        }
        startPos = gameObject.transform.position;
        sprite.flipX = false;
        extraJumps = extraJumpValue;
        rb = GetComponent<Rigidbody2D>();
        nickName.text = photonView.Owner.NickName;
        isOnRope = false;
        slowTimer = 3f;
        startSpeed = speed;
    }

    private void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
            isWater = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsWater);
            
            moveInput = Input.GetAxis("Horizontal");
            ropeInput = Input.GetAxis("Vertical");

            if (classEnum == ClassEnum.WARRIOR)
            {
                if (!dash.isDashing)
                {
                    rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
                }
            }
            else
            {
                rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
            }

            if (moveInput > 0)
            {
                photonView.RPC("FlipRight", RpcTarget.All, null);
                facingRight = true;
            }
            else if (moveInput < 0)
            {
                photonView.RPC("FlipLeft", RpcTarget.All, null);
                facingRight = false;
            }
        }
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            if (isGrounded)
            {
                extraJumps = extraJumpValue;
            }
            
            if (Input.GetKeyDown(KeyCode.Space) && extraJumps >= 0 && isWater)
            {
                rb.velocity = Vector2.up * jumpForce;
            }

            if (Input.GetKeyDown(KeyCode.Space) && extraJumps >= 0 && isOnRope)
            {
                rb.velocity = Vector2.up * jumpForce;
            }
            
            if (Input.GetKeyDown(KeyCode.Space) && extraJumps >= 0 && isGrounded)
            {
                rb.velocity = Vector2.up * jumpForce;
            }
            else if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0 && !isGrounded)
            {
                rb.velocity = Vector2.up * jumpForce;
                extraJumps--;
            }

            if (isSlowed)
            {
                slowTimer -= Time.deltaTime;
                if (slowTimer <= 0)
                {
                    isSlowed = false;
                    speed = startSpeed;
                    slowTimer = 3f;
                }
            }
        }
    }

    [PunRPC]
    void FlipLeft()
    {
        sprite.flipX = false;
        if (classEnum == ClassEnum.WARRIOR)
        {
            animator.SetBool("isFacingRight", false);
        }
    }

    [PunRPC]
    void FlipRight()
    {
        sprite.flipX = true;
        if (classEnum == ClassEnum.WARRIOR)
        {
            animator.SetBool("isFacingRight", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Dummy")
        {
            Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), this.GetComponent<Collider2D>());
        }

        if (other.gameObject.tag == "Water")
        {
            extraJumps = extraJumpValue;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Rope")
        {
            isOnRope = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Rope")
        {
            isOnRope = false;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Rope")
        {
            rb.velocity = new Vector2(rb.velocity.x, ropeInput * (speed / 1.5f));
        }
    }
}
