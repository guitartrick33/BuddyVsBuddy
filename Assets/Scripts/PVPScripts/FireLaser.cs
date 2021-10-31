using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class FireLaser : MonoBehaviourPunCallbacks
{
    Meteor meteor;
    Fireball fireball;

    public float cooldownTime;
    private float nextFireTime = 0f;

    public bool castinglaser = false;
    public float castTimelaser = 1f;
    private float timerCastTimelaser;

    public bool isOnCd = false;
    private float cooldownTimer;

    private Image firelaserLoading;
    private Text fireLaserCd;

    [SerializeField] ParticleSystem castParticles;

    public GameObject spell;
    public Transform firePoint;
    public float spellSpeed = 50;
    public GameObject player;
    private GameObject spellClone;

    Vector3 lookDirection;
    float lookAngle;

    private PhotonView photonView;



    private void Start()
    {
        firelaserLoading = GameObject.FindWithTag("LaserImage").GetComponent<Image>();
        fireLaserCd = GameObject.FindWithTag("LaserText").GetComponent<Text>();
        timerCastTimelaser = castTimelaser;
        meteor = GetComponent<Meteor>();
        fireball = GetComponent<Fireball>();
        cooldownTimer = cooldownTime;
        photonView = gameObject.GetComponent<PhotonView>();
    }

    void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        
        if (photonView.IsMine)
        {
            lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - new Vector3(player.transform.position.x, player.transform.position.y - 0.70f);
            lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
            
            firePoint.rotation = Quaternion.Euler(0, 0, lookAngle);
            
            firelaserLoading.fillAmount = 0;
            if (meteor.casting == false && fireball.castingFireBall == false)
            {
                
                if (timerCastTimelaser <= 0)
                {
                    spellClone = PhotonNetwork.Instantiate("FireLaser", lookDirection, Quaternion.identity, 0);
                    spellClone.transform.position = firePoint.position;
                    spellClone.transform.rotation = Quaternion.Euler(0, 0, lookAngle);
                    spellClone.GetComponent<Rigidbody2D>().velocity = firePoint.right * spellSpeed;

                    nextFireTime = Time.time + cooldownTime;
                    cooldownTimer = cooldownTime;
                    timerCastTimelaser = castTimelaser;
                    castinglaser = false;
                    photonView.RPC("StopParticlesLaser", RpcTarget.All, null);
                }
    
                if (Time.time > nextFireTime)
                {
                    if (castinglaser)
                    {
                        timerCastTimelaser -= Time.deltaTime;
                    }
                    if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        castinglaser = true;
                        photonView.RPC("StartParticlesLaser", RpcTarget.All, null);
                    }
                    isOnCd = false;
                }
                
            }
            if (Time.time <= nextFireTime)
            {
                isOnCd = true;
    
            }
            if (isOnCd)
            {
                cooldownTimer -= Time.deltaTime;
                firelaserLoading.fillAmount = cooldownTimer / cooldownTime;
                fireLaserCd.text = (cooldownTimer % cooldownTime).ToString();
                if (firelaserLoading.fillAmount <= 0.05)
                {
                    fireLaserCd.text = "";
                }
            }
            else
            {
                firelaserLoading.fillAmount = -1;
            }
        }
       
    }

    [PunRPC]
    void Shoot()
    {
        spellClone.transform.position = firePoint.position;
        spellClone.transform.rotation = Quaternion.Euler(0, 0, lookAngle);
        spellClone.GetComponent<Rigidbody2D>().velocity = firePoint.right * spellSpeed;
    }


    [PunRPC] private void StartParticlesLaser()
    {
        castParticles.Play();
    }

    [PunRPC]
    void StopParticlesLaser()
    {
        castParticles.Stop();
    }
}
