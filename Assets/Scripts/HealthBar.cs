using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    private PhotonView photonView;

    private void Start()
    {
        photonView = gameObject.GetComponent<PhotonView>();
    }

    public void SetHealthRPC(float health)
    {
        photonView.RPC("SetHealth", RpcTarget.All, health);
    }
    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    [PunRPC]
    public void SetHealth(float health)
    {
        slider.value = health;
    }
}
