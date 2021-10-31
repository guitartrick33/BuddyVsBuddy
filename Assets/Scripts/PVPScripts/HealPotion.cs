using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPotion : MonoBehaviour
{
    public float healAmount = 300f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !other.gameObject.CompareTag("Bullet"))
        {
            other.gameObject.GetComponent<PlayerResources>().currentHealth += healAmount;
            Destroy(gameObject);
        }
    }
}
