using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class Shield : MonoBehaviour
{
    public bool isShieldActive;
    
    public float shieldDuration;
    public float shieldDurationTimer;

    public float cooldown;
    public float cooldownTimer;
    private bool isOnCd;

    private PhotonView photonView;
    public GameObject shieldImage;

    private Image shieldCdImage;
    private Text shieldCDText;
    

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        if (photonView.IsMine)
        {
            isShieldActive = false;
            shieldDurationTimer = shieldDuration;
            cooldownTimer = cooldown;
            photonView.RPC("DeactivateImage", RpcTarget.All, null);
            shieldCdImage = GameObject.FindWithTag("shieldImage").GetComponent<Image>();
            shieldCDText = GameObject.FindWithTag("shieldText").GetComponent<Text>();
        }
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.Alpha3) && !isOnCd && !gameObject.GetComponent<PlayerRespawner>().hasRespawned)
            {
                photonView.RPC("ShieldIsActive", RpcTarget.All, null);
                photonView.RPC("ActivateImage", RpcTarget.All, null);
            }

            if (isShieldActive)
            {
                shieldDurationTimer -= Time.deltaTime;
            }

            if (shieldDurationTimer <= 0)
            {
                photonView.RPC("ShieldIsNotActive", RpcTarget.All, null);
                shieldDurationTimer = shieldDuration;
                isOnCd = true;
                photonView.RPC("DeactivateImage", RpcTarget.All, null);
            }

            if (isOnCd)
            {
                cooldownTimer -= Time.deltaTime;
                shieldCdImage.fillAmount = cooldownTimer / cooldown;
                shieldCDText.text = (cooldownTimer % cooldown).ToString();
                if (shieldCdImage.fillAmount <= 0.05)
                {
                    shieldCDText.text = "";
                }
            }
            else
            {
                shieldCdImage.fillAmount = -1;
            }

            if (cooldownTimer <= 0)
            {
                isOnCd = false;
                cooldownTimer = cooldown;
            }
        }
    }

    [PunRPC]
    void ShieldIsActive()
    {
        isShieldActive = true;
    }
    
    [PunRPC]
    void ShieldIsNotActive()
    {
        isShieldActive = false;
    }

    [PunRPC]
    void ActivateImage()
    {
        shieldImage.SetActive(true);
    }
    
    [PunRPC]
    void DeactivateImage()
    {
        shieldImage.SetActive(false);
    }
}
