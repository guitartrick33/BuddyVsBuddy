using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class Fireball : MonoBehaviour
{
    Meteor meteor;

    public float cooldownTime = 0.8f;
    private float nextFireTime = 0f;

    public bool castingFireBall = false;
    public float castTimeFireBall;
    private float timerCastTimeFireBall;

    public bool isOnCd = false;
    private float cooldownTimer;

    private Image fireBallLoading;
    private Text fireBallCd;
    private Image fireOverlay;

    [SerializeField] ParticleSystem castParticles;

    public GameObject spell;
    public Transform firePoint;
    public float spellSpeed = 50;
    public GameObject player;

    Vector2 lookDirection;
    float lookAngle;

    private PhotonView photonView;

    private void Start()
    {
        fireBallLoading = GameObject.FindWithTag("FBimage").GetComponent<Image>();
        fireBallCd = GameObject.FindWithTag("FBtext").GetComponent<Text>();
        fireOverlay = GameObject.FindWithTag("FBfire").GetComponent<Image>();
        timerCastTimeFireBall = castTimeFireBall;
        meteor = GetComponent<Meteor>();
        cooldownTimer = cooldownTime;
        fireOverlay.enabled = false;
        photonView = gameObject.GetComponent<PhotonView>();
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - new Vector3(player.transform.position.x, player.transform.position.y - 0.70f);
            lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

            fireBallLoading.fillAmount = 0;
            if(meteor.casting == false)
            {
                if (timerCastTimeFireBall <= 0.0f)
                {
                    Shoot();
                    nextFireTime = Time.time + cooldownTime;
                    cooldownTimer = cooldownTime;
                    timerCastTimeFireBall = castTimeFireBall;
                    castingFireBall = false;
                    fireOverlay.enabled = false;
                    photonView.RPC("StopParticles", RpcTarget.All, null);
                }
                if (Time.time > nextFireTime)
                {
                    if(castingFireBall == true)
                    {
                        timerCastTimeFireBall -= Time.deltaTime;
                    }
                    if (Input.GetKeyDown(KeyCode.Alpha2))
                    {
                        castingFireBall = true;
                        fireOverlay.enabled = true;
                        photonView.RPC("StartParticles", RpcTarget.All, null);
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
                fireBallLoading.fillAmount = cooldownTimer / cooldownTime;
                fireBallCd.text = (cooldownTimer % cooldownTime).ToString();
                if (fireBallLoading.fillAmount <= 0.03)
                {
                    fireBallCd.text = "";
                }
            }
            
            else
            {
                fireBallLoading.fillAmount = -1;
            }
        }
    }

    void Shoot()
    {
        GameObject spellClone = PhotonNetwork.Instantiate("FireBall", lookDirection, Quaternion.identity, 0);
        spellClone.transform.position = firePoint.position;
        spellClone.transform.rotation = Quaternion.Euler(0, 0, lookAngle);

        spellClone.GetComponent<Rigidbody2D>().velocity = firePoint.right * spellSpeed;
    }
    
    [PunRPC]
    void StartParticles()
    {
        castParticles.Play();
    }

    [PunRPC]
    void StopParticles()
    {
        castParticles.Stop();
    }
    
}
