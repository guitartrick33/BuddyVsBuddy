using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Conflagarate : MonoBehaviour
{
    Meteor meteor;
    Fireball fireball;


    public float cooldownTime;
    private float nextFireTime = 0f;

    public bool castingConflagate = false;
    public float castTimeConflagate = 1f;
    private float timerCastTimeConflagate;

    public bool isOnCd = false;
    private float cooldownTimer;
    
    public Image conflagarateLoading;
    public Text conflagarateCd;

    [SerializeField] ParticleSystem castParticles;

    public GameObject spell;
    public Transform firePoint;
    public float spellSpeed = 50;
    public GameObject player;

    Vector2 lookDirection;
    float lookAngle;

    private void Start()
    {
        timerCastTimeConflagate = castTimeConflagate;
        meteor = GetComponent<Meteor>();
        fireball = GetComponent<Fireball>();
        cooldownTimer = cooldownTime;
    }

    void Update()
    {
        lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - new Vector3(player.transform.position.x, player.transform.position.y - 1.25f);
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

        firePoint.rotation = Quaternion.Euler(0, 0, lookAngle);
        conflagarateLoading.fillAmount = 0;
            if (meteor.casting == false && fireball.castingFireBall == false)
            {
                
                if (timerCastTimeConflagate <= 0)
                {
                    GameObject spellClone1 = Instantiate(spell);
                    spellClone1.transform.position = firePoint.position;
                    spellClone1.transform.rotation = Quaternion.Euler(0, 0, lookAngle);

                    spellClone1.GetComponent<Rigidbody2D>().velocity = firePoint.right * spellSpeed;
                    
                    nextFireTime = Time.time + cooldownTime;
                    cooldownTimer = cooldownTime;
                    timerCastTimeConflagate = castTimeConflagate;
                    castingConflagate = false;
                    castParticles.Stop();
                }

                if (Time.time > nextFireTime)
                {
                    if (castingConflagate == true)
                    {
                        timerCastTimeConflagate -= Time.deltaTime;
                    }
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        castingConflagate = true;
                        castParticles.Play();
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
                conflagarateLoading.fillAmount = cooldownTimer / cooldownTime;
                conflagarateCd.text = (cooldownTimer % cooldownTime).ToString();
                if (conflagarateLoading.fillAmount <= 0.05)
                {
                    conflagarateCd.text = "";
                }
            }
        else
        {
            conflagarateLoading.fillAmount = -1;
        }
    }
}
