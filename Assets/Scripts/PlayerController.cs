using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviourPunCallbacks
{
    public Vector3 startPos;
    private PhotonView photonView;

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
    public LayerMask whatIsLava;

    private int extraJumps;
    public int extraJumpValue;

    private bool facingRight = true;

    public Text nickName;
    public SpriteRenderer sprite;
    
    void Start()
    {
        startPos = gameObject.transform.position;
        sprite.flipX = false;
        extraJumps = extraJumpValue;
        photonView = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody2D>();
        nickName.text = photonView.Owner.NickName;
        fd = gameObject.GetComponent<FallDamage>();
        isOnRope = false;
    }

    private void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
            isWater = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsWater);
            isLava = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsLava);
            
            moveInput = Input.GetAxis("Horizontal");
            ropeInput = Input.GetAxis("Vertical");
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

            if (moveInput > 0)
            {
                photonView.RPC("FlipRight", RpcTarget.All, null);
            }
            else if (moveInput < 0)
            {
                photonView.RPC("FlipLeft", RpcTarget.All, null);
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
        }
    }

    [PunRPC]
    void FlipLeft()
    {
        sprite.flipX = true;
    }

    [PunRPC]
    void FlipRight()
    {
        sprite.flipX = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
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
            fd.startYPos = gameObject.transform.position.y;
            isOnRope = false;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Rope")
        {
            rb.velocity = new Vector2(rb.velocity.x, ropeInput * speed);
        }
    }
}
