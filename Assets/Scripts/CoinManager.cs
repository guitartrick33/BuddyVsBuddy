using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Dummy")
        {
            other.gameObject.GetComponent<PlayerResources>().currentTalentPoints++;
        }
    }
}
