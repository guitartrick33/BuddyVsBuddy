using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class Meteor : MonoBehaviour
{
    Fireball fireball;

    public float cooldownTime = 0.8f;
    private float nextFireTime = 0f;

    public bool casting = false;
    public float castTime;
    private float timerCastTime;

    public bool isOnCd = false;
    private float cooldownTimer;

    private Image meteorLoading;
    private Text meteorCd;
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
        meteorLoading = GameObject.FindWithTag("MeteorImage").GetComponent<Image>();
        meteorCd = GameObject.FindWithTag("MeteorText").GetComponent<Text>();
        fireOverlay = GameObject.FindWithTag("MeteorFire").GetComponent<Image>();
        timerCastTime = castTime;
        fireball = GetComponent<Fireball>();
        cooldownTimer = cooldownTime;
        fireOverlay.enabled = false;
        photonView = gameObject.GetComponent<PhotonView>();
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - new Vector3(player.transform.position.x, player.transform.position.y - 1.25f);
                    lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
            
                    firePoint.rotation = Quaternion.Euler(0, 0, lookAngle);
                    
                    meteorLoading.fillAmount = 0;
                    if (fireball.castingFireBall == false)
                    {
                        if (timerCastTime <= 0.0f)
                        {
                            Shoot();
                            nextFireTime = Time.time + cooldownTime;
                            cooldownTimer = cooldownTime;
                            timerCastTime = castTime;
                            casting = false;
                            fireOverlay.enabled = false;
                            photonView.RPC("StopParticlesMeteor", RpcTarget.All, null);
                        }
                        if (Time.time > nextFireTime)
                        {
                            if (casting == true)
                            {
                                timerCastTime -= Time.deltaTime;
                            }
                            if (Input.GetKeyDown(KeyCode.Alpha3))
                            {
                                photonView.RPC("StartParticlesMeteor", RpcTarget.All, null);
                                casting = true;
                                fireOverlay.enabled = true;
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
                        meteorLoading.fillAmount = cooldownTimer / cooldownTime;
                        meteorCd.text = (cooldownTimer % cooldownTime).ToString();
                        if (meteorLoading.fillAmount <= 0.03f)
                        {
                            meteorCd.text = "";
                        }
                    }
            
                    else
                    {
                        meteorLoading.fillAmount = -1;
                    }
        }
    }

    void Shoot()
    {
        GameObject spellClone = PhotonNetwork.Instantiate("Meteor", lookDirection, Quaternion.identity, 0);
        spellClone.transform.position = firePoint.position;
        spellClone.transform.rotation = Quaternion.Euler(0, 0, lookAngle);

        spellClone.GetComponent<Rigidbody2D>().velocity = firePoint.right * spellSpeed;
    }

    [PunRPC]
    void StartParticlesMeteor()
    {
        castParticles.Play();
    }

    [PunRPC]
    void StopParticlesMeteor()
    {
        castParticles.Stop();
    }
}
