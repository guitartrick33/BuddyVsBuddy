using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class AxeThrow : MonoBehaviour
{
    public float cooldown;

   
    private float timerCastTimelaser;

    public bool isOnCd = false;
    private float cooldownTimer;

    public Transform firePoint;
    public float speed = 50;
    public GameObject player;

    Vector3 lookDirection;
    float lookAngle;

    private PhotonView photonView;

    private Animator animator;

    private Image axeImage;
    private Text axeText;


    private void Start()
    {
        photonView = gameObject.GetComponent<PhotonView>(); 
        if (photonView.IsMine)
        {
            cooldownTimer = cooldown;
            animator = GetComponent<Animator>();
            axeImage = GameObject.FindWithTag("axeImage").GetComponent<Image>();
            axeText = GameObject.FindWithTag("axeText").GetComponent<Text>();
        }
    }

    void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        if (photonView.IsMine)
        {
            lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) -
                            new Vector3(player.transform.position.x, player.transform.position.y);
            lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
            firePoint.rotation = Quaternion.Euler(0, 0, lookAngle);

            if (Input.GetKeyDown(KeyCode.Alpha2) && !isOnCd && !gameObject.GetComponent<PlayerRespawner>().hasRespawned)
            {
                GameObject spellClone = PhotonNetwork.Instantiate("AxeThrow", lookDirection, Quaternion.identity, 0);
                spellClone.transform.position = firePoint.position;
                spellClone.transform.rotation = Quaternion.Euler(0, 0, lookAngle);
                spellClone.GetComponent<Rigidbody2D>().velocity = firePoint.right * speed;
                isOnCd = true;
                animator.SetTrigger("Attack");
            }

            if (isOnCd)
            {
                cooldownTimer -= Time.deltaTime;
                axeImage.fillAmount = cooldownTimer / cooldown;
                axeText.text = (cooldownTimer % cooldown).ToString();
                if (axeImage.fillAmount <= 0.05)
                {
                    axeText.text = "";
                }
            }
            else
            {
                axeImage.fillAmount = -1;
            }

            if (cooldownTimer <= 0)
            {
                isOnCd = false;
                cooldownTimer = cooldown;
            }

        }

    }
}
