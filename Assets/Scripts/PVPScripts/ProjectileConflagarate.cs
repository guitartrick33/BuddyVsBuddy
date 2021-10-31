using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileConflagarate : MonoBehaviour
{
    public GameObject impactEffect;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("Item") && !collision.gameObject.CompareTag("Door") && !collision.gameObject.CompareTag("TutorialItem") && !collision.gameObject.CompareTag("Bullet") && !collision.gameObject.CompareTag("Potion"))
        {
            Instantiate(impactEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Dummy"))
        {
            Instantiate(impactEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }

        
    }
}
