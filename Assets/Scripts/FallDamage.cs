using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class FallDamage : MonoBehaviour
{
    public float startYPos;
    public float endYpos;
    public float damageThreshold = 10;
    public bool damageMe = false;
    public bool firstCall = true;
    private PlayerResources pr;
    private PlayerMovement pc;
    private PhotonView photonView;
    public float damageMultiplier = 1;

    private void Start()
    {
        pr = gameObject.GetComponent<PlayerResources>();
        pc = gameObject.GetComponent<PlayerMovement>();
        photonView = gameObject.GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            if (gameObject.transform.position.y > startYPos)
            {
                firstCall = true;
            }

            if (!pc.isGrounded)
            {
                if (firstCall)
                {
                    startYPos = gameObject.transform.position.y;
                    firstCall = false;
                    damageMe = true;
                }
            }

            if (pc.isGrounded)
            {
                endYpos = gameObject.transform.position.y;
                if (startYPos - endYpos > damageThreshold)
                {
                    if (damageMe)
                    {
                        // pr.TakeDamage((startYPos - endYpos - damageThreshold) * damageMultiplier);
                        damageMe = false;
                        firstCall = true;
                    }
                }
            }
            if (pc.isWater)
            {
                startYPos = gameObject.transform.position.y;
                endYpos = gameObject.transform.position.y;
            }

            if (pc.isLava)
            {
                // pr.TakeDamage(pr.maxHealth);
            }
        }
    }
}
